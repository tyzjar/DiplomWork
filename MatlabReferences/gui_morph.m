function gui_morph(logfile, expname, varargin)
% folder - path to the experiment folder
% order - cell 2xN order{i,1}{j} brains are to be morphed to order{i,2}
t=readtable(expname);
Ng=size(t,1);

if isempty(varargin)
    age  = 'adult';
else
    age  = varargin{1}
end

LogProcess(logfile,'Progressbar',num2str(0),num2str(Ng));

for i=1:Ng
  
    OriginalSample = strcat(cell2mat(t{i,2}),'\*.tif');        % name of folder with original sample images
    RegistrationSample =strcat(cell2mat(t{i,1}),'\*.tif');     % name of folder with registration sample images
    SaveFolder = cell2mat(t{i,3});                             % name of folder to save files
    TSave = cell2mat(t{i,4});                                  % name of file to save transformations
    NIISave = cell2mat(t{i,5});                                % name of file to save 3d image in nii format
    
    LogProcess(logfile,'Operation',strcat('Sample', 32, '"', RegistrationSample, '"'));
    LogProcess(logfile,'SampleName',strcat('is registering in sample', 32, '"', OriginalSample, '"'));
    
    IN=Rload(OriginalSample , 1, 1.6)/65535;
    [M,N,L]=size(IN);
    im=Rload(RegistrationSample , 1, 1.6)/65535;
    [m,n,l]=size(im);
    
    while true
       dx=M-m;
       dy=N-n;
       dz=L-l; 
       minDelta = min([dx/m, dy/n, dz/l]);
       coef = 1;
       
       if minDelta < 0    
           switch minDelta
               case dx/m
                   coef = round(m / (m + dx),2) + 0.01;
               case dy/n
                   coef = round(n / (n + dy),2) + 0.01;
               case dz/l
                   coef = round(l / (l + dz),2) + 0.01;
           end
           fprintf('dx=%d dy=%d dz=%d coef=%d\n',dx,dy,dz,coef);
           im=Rload(RegistrationSample, coef, 1.6)/65535;
           [m,n,l]=size(im);
       end
       
       if minDelta >= 0
         break;
       end
       
    end
    
    tic
    [m,n,l]=size(im);
    [M,N,L]=size(IN);
    IM=zeros(M,N,L);
    a=ceil((M-m)/2);
    b=ceil((N-n)/2);
    c=ceil((L-l)/2);

    fprintf('WHO x=%d y=%d z=%d\n',m,n,l);
    fprintf('IN x=%d y=%d z=%d\n',M,N,L);

    IM(a+1:a+m,b+1:b+n,c+1:c+l)=im;
    nltfBA = mnlinsiman_resize(IM,IN,age,'full');
    IM=mnlintform(IM, nltfBA);
    mkdir(SaveFolder);
    save(strcat(SaveFolder,'\',TSave),'nltfBA');
    RprepareNii(IM*65535,strcat(SaveFolder,'\', NIISave));
    for k=1:size(IM,3)
        imwrite(uint16(IM(:,:,k)*65535),strcat(SaveFolder,'\img_', sprintf('%06d.tif',k)));
    end
    toc
    LogProcess(logfile,'Progressbar',num2str(i),num2str(Ng));
end

end