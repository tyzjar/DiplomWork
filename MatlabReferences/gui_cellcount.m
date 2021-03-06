function gui_cellcount(logfile, expname, HIPASS, LOWPASS, THRESHOLD, MINREG, CONFLVL, SMIN, SMAX, koef)
    
%Predefined values for sstat

NTR       = 1000;         %Number of bootstrap trials
R         = 7;            %Radius of the region for the bootstrap

%LOWPASS   = options{1,2};            %Low-pass filter standard deviation (in pixels) to reduce pixel noise
%HIPASS    = options{2,2};          %Hi-pass filter standard deviation (in pixels): substantially larger than the cell
%THRESHOLD = options{3,2};            %Intensity threshold (absolute value): no threshold
%MINREG    = options{4,2};          %Minimal region size (in voxels) to calculate the statistics

%Predefined values for sselect

%CONFLVL   = options{5,2};          %P-value for the putative cell to match the following criteria
%SMIN      = options{6,2};            %Standard deviations of the data (in pixels), fitted with Gaussian distribution:
%SMAX      = options{7,2};
%koef      = options{8,2};            %S1 to S3

S1MIN     = SMIN;         
S1MAX     = SMAX;          
S2MIN     = SMIN;
S2MAX     = SMAX;
S3MIN     = SMIN*koef;
S3MAX     = SMAX*koef;
IMMIN     = 0.3;            %Signal intensity: no limits
IMMAX     = Inf;

%Task splitting parameters
INTERACT  = 0;   
blsz      = 64;          %Splitting the task into squares of the given size (in pixels) to fit the RAM.
alfa      = 0.2;         %Overlap 

imsubfolder = 'ImarisView';

myList = table2cell(readtable(expname));

%Number of samples
Ng = size(myList,1);

LogProcess(logfile,'Operation','Cell isolation and counting');
LogProcess(logfile,'SampleName','');
LogProcess(logfile,'Progressbar',num2str(0),num2str(1));

for u=1:Ng
    %чтение перфого файла, определение размеров Исправить Vblock, чтобы не
    %считывать файл каждый раз
    
    LogProcess(logfile,'SampleName',myList{u,1});
    tmp = myList{u,1};
    chdir(tmp);
    Imarisfolder = strcat(myList{u,1},'\',imsubfolder,'\');
    
    fol=dir('*.tif');
    IN=imread(fol(1).name);
    L=length(fol);
    [N,M]=size(IN);
    %[N,M,L] sizes of total VOLUME
    %block is part of this VOLUME with i,j,k, index
    MAXi=ceil((N/blsz-1)/(1-alfa)+1);
    MAXj=ceil((M/blsz-1)/(1-alfa)+1);
    MAXk=ceil((L/blsz-1)/(1-alfa)+1);
    OVERLAP=blsz*alfa*0.5;

    X = [];
    Y = [];
    Z = [];
    primitiveBar = waitbar(0,'0','Name', 'Cell detection');
    primitiveBar.Children.Title.Interpreter = 'none';

    for k=1 : MAXk
        for j = 1 : MAXj
            waitbar(((k-1)*MAXj+(j-1))/(MAXk*MAXj), primitiveBar,...
                sprintf('[%d / %d ]',((k-1)*MAXj+(j-1))*MAXi, MAXk*MAXj*MAXi));
            parfor i = 1 : MAXi
                fprintf('region (%d, %d, %d) of (%d, %d, %d)\n', i, j, k, MAXi, MAXj,MAXk)

                [im,bstart] =block( blsz, alfa,i,j,k,fol,N,M,L);

                [S, ~, ~] = sstat(im, NTR, R, THRESHOLD, LOWPASS, HIPASS, MINREG, 0);
                [x, y, z] = sselect(im, S, CONFLVL, S1MIN, S1MAX, S2MIN, S2MAX,...
                    S3MIN, S3MAX, IMMIN, IMMAX, INTERACT);

                %Selecting spots with no overlap
                ind = find((x >= 1 + sign(i - 1) * OVERLAP) .* ...
                        (x <=  blsz + sign(i - MAXi) * OVERLAP) .* ...
                        (y >= 1 + sign(j - 1) * OVERLAP) .* ...
                        (y <= blsz+sign(j - MAXj) * OVERLAP).* ...
                        (z >= 1 + sign(k - 1) * OVERLAP) .* ...
                        (z <=  blsz + sign(k - MAXi) * OVERLAP));

                % плохая оптимизация. Нужно правильное разбиение.
                X = [X, x(ind)+bstart(1)-1];
                Y = [Y, y(ind)+bstart(2)-1];
                Z = [Z, z(ind)+bstart(3)-1];
                     
            end
        end
        T=table(uint16(X'),uint16(Y'),uint16(Z'),'VariableNames',{'X','Y','Z'});
        writetable(T,sprintf('cells1-%06d.txt',k));
        delete(sprintf('cells1-%06d.txt',k-1));
    end
    
    T=table(uint16(X'),uint16(Y'),uint16(Z'),'VariableNames',{'X','Y','Z'});
    writetable(T,sprintf('cells.txt'));
    
    waitbar(1, primitiveBar,...
        [sprintf('[%d / %d ]',MAXk*MAXj*MAXi, MAXk*MAXj*MAXi)]);
    delete(primitiveBar);
    LogProcess(logfile,'Progressbar',num2str(u),num2str(Ng));
    
    %Save for Imaris
    mkdir(myList{u,1},imsubfolder);
    
    ImarisMatrix = zeros(N,M,L);
    for s=1:size(X,2)
        ImarisMatrix(X(s),Y(s),Z(s)) = 1*65535;
    end
    
    for s=1:L
        imwrite(uint16(ImarisMatrix(:,:,s)),strcat(Imarisfolder, sprintf('Im%06d.tif',s)));
    end
    
end %end u:Ng

end

