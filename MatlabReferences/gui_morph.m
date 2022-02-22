function gui_morph(logfile,expname,varargin)
% folder - path to the experiment folder
% order - cell 2xN order{i,1}{j} brains are to be morphed to order{i,2}
t=readtable(expname);
Ng=size(t,1);
if isempty(varargin)
    InF  = 'Masked'; % name of INPUT  subfolder 
    OutF = 'Morph' ; % name of OUTPUT subfolder
    age  = 'adult';
else
    InF  = varargin{1} % name of INPUT  subfolder 
    OutF = varargin{2} % name of OUTPUT subfolder
    age  = varargin{3}
end

LogProcess(logfile,'Progressbar',num2str(0),num2str(Ng));

for i=1:Ng
    br_name2=cell2mat(t{i,2});
    st=strfind(br_name2,'\');
    br_name2=br_name2(st(end)+1:end);
    br_name1=cell2mat(t{i,1});
    st=strfind(br_name1,'\');
    br_name1=br_name1(st(end)+1:end);
    
    LogProcess(logfile,'Operation',strcat('Sample', 32, '"', cell2mat(t{i,1}), '"'));
    LogProcess(logfile,'SampleName',strcat('morphing in sample', 32, '"',cell2mat(t{i,2}), '"'));
    
    IN=Rload(strcat(cell2mat(t{i,2}),'\*.tif'),1,1)/65535;
    [M,N,L]=size(IN);
    im=Rload(strcat(cell2mat(t{i,1}),'\',InF,'\*.tif'),1,1)/65535;
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

    fprintf('IN x=%d y=%d z=%d\n',M,N,L);
    fprintf('WHO x=%d y=%d z=%d\n',m,n,l);

    IM(a+1:a+m,b+1:b+n,c+1:c+l)=im;
    nltfBA = mnlinsiman_resize(IM,IN,age,'full');
    IM=mnlintform(IM, nltfBA);
    mkdir(strcat(cell2mat(t{i,1}),'\'),OutF);
    save(strcat(cell2mat(t{i,1}),'\',br_name1,'_to_',...
        br_name2,'.mat'),'nltfBA');
    %RprepareNii(IM*65535,strcat(folder,'\','S_',order{i,1}{j},'_to_',order{i,2},'.nii'));
    for k=1:size(IM,3)
        imwrite(uint16(IM(:,:,k)*65535),strcat(cell2mat(t{i,1}),...
            '\',OutF,'\',br_name1,'_', sprintf('%06d.tif',k)));
    end
    toc
    LogProcess(logfile,'Progressbar',num2str(i),num2str(Ng));
end

end