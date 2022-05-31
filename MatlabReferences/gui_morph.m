function gui_morph(logfile, expname, varargin)
% folder - path to the experiment folder
% order - cell 2xN order{i,1}{j} brains are to be morphed to order{i,2}
t=readtable(expname);
Ng=size(t,1);

if isempty(varargin)
    OutF  = 'Morph' ;     % name of OUTPUT subfolder
    age   = 'adult';
else
    OutF  = varargin{1}   % name of OUTPUT subfolder
    age   = varargin{2}
end

LogProcess(logfile,'Progressbar',num2str(0),num2str(Ng));

for i=1:Ng
    LogProcess(logfile,'Operation',strcat('Sample', 32, '"', cell2mat(t{i,1}), '"'));
    LogProcess(logfile,'SampleName',strcat('is registering in sample', 32, '"',cell2mat(t{i,2}), '"'));
    
    TSave = cell2mat(t{i,3});   % name of file to save transformations
    NIISave = cell2mat(t{i,4});   % name of file to save transformations
    
    IN=Rload(strcat(cell2mat(t{i,2}),'\*.tif'),1,1)/65535;
    [M,N,L]=size(IN);
    im=Rload(strcat(cell2mat(t{i,1}),'\*.tif'),1,1)/65535;
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
           im=Rload(strcat(cell2mat(t{i,1}),'\',InF,'\*.tif'),coef,1)/65535;
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
    mkdir(strcat(cell2mat(t{i,1}),'\'),OutF);
    save(strcat(cell2mat(t{i,1}),'\',TSave),'nltfBA');
    RprepareNii(IM*65535,strcat(cell2mat(t{i,1}),'\', NIISave));
    for k=1:size(IM,3)
        imwrite(uint16(IM(:,:,k)*65535),strcat(cell2mat(t{i,1}),...
            '\',OutF,'\img_', sprintf('%06d.tif',k)));
    end
    toc
    LogProcess(logfile,'Progressbar',num2str(i),num2str(Ng));
end

end