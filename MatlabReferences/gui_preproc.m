function gui_preproc(logfile, expname, subfolder, midlvl)

yes = 'y';
done = 'd';

SettingsColumn = 1;
SampleColumn = 2;
SubtractionColumn = 3;
IntensityColumn = 4;
CropColumn = 5;
MaskColumn = 6;

myList=readtable(expname);

%Number of samples
Ng = str2num(cell2mat(myList{1,SettingsColumn}));

%Load Image if need
ImgName = cell2mat(myList{2,SettingsColumn});
ImgLength = strlength(ImgName);
if (ImgLength>0)
  BG = imread(ImgName);
end

folnameSubtraction = cell2mat(myList{3,SettingsColumn});
folnameIntensity = cell2mat(myList{4,SettingsColumn});
folnameCrop = cell2mat(myList{5,SettingsColumn});
folnameMask = cell2mat(myList{6,SettingsColumn});

g=zeros(1472,2184);
g(704,1295)=1;
G=mgauss(g,600);
G=G./max(G(:));
    


%Subtraction picture
%Leveling Intensity
LoadTmp = strings (1, Ng);
LogProcess(logfile,'Operation','Leveling Intensity and Subtraction picture process');
LogProcess(logfile,'SampleName','');
LogProcess(logfile,'Progressbar',num2str(0),num2str(Ng));

for i=1:Ng
    sample = cell2mat(myList{i,SampleColumn});
    tmp=strcat(sample,'\',subfolder);
    LoadTmp(i) = tmp;
    
    SubtractionPicture = strcmp(yes,cell2mat(myList{i,SubtractionColumn})) && ImgLength>0;
    LevelingIntensity = strcmp(yes,cell2mat(myList{i,IntensityColumn}));
    
    if(SubtractionPicture)
        mkdir(sample,folnameSubtraction);
        LoadTmp(i) = strcat(sample,'\',folnameSubtraction);
        LogProcess(logfile,'SampleName',sample);
    else
        if(strcmp(done,cell2mat(myList{i,SubtractionColumn})))
            LoadTmp(i) = strcat(sample,'\',folnameSubtraction);
        end
    end
    
    if(LevelingIntensity)
        mkdir(sample,folnameIntensity);
        LoadTmp(i) = strcat(sample,'\',folnameIntensity);
        if(~SubtractionPicture)
            LogProcess(logfile,'SampleName',sample);
        end
    else
        if(strcmp(done,cell2mat(myList{i,IntensityColumn})))
            LoadTmp(i) = strcat(sample,'\',folnameIntensity);
        end 
    end
    
    if(SubtractionPicture || LevelingIntensity)
        files=dir(strcat(tmp,'\*tif'));
        for j=1:length(files)
            IN=double(imread(strcat(tmp,'\',files(j).name)));
            %Subtraction picture
            if(SubtractionPicture)
                IN=IN.*(BG<8);
                imwrite(uint16(IN),strcat(sample,'\',folnameSubtraction,'\',files(j).name));
            end
            %Leveling Intensity
            if(LevelingIntensity)
                imwrite(uint16(IN./G),strcat(sample,'\',folnameIntensity,'\',files(j).name));              
            end
        end
    end
    
    LogProcess(logfile,'Progressbar',num2str(i),num2str(Ng));
end

%Crop
LogProcess(logfile,'Operation','Crop process');
LogProcess(logfile,'SampleName','');
LogProcess(logfile,'Progressbar',num2str(0),num2str(Ng));

for i=1:Ng
    sample = cell2mat(myList{i,SampleColumn});
    fprintf('%s\n' , LoadTmp(i));
    if(strcmp(yes,cell2mat(myList{i,CropColumn})))
        LogProcess(logfile,'SampleName',sample);
        mkdir(sample,folnameCrop);
        upview=Vupview2(LoadTmp(i),20,br_name(sample));
        figure('Name',LoadTmp(i),'NumberTitle','off')
        [~,r]=imcrop(upview);
        title('Chanel');
        close
        Vcrop(LoadTmp(i),strcat(sample,'\',folnameCrop),r);
        LoadTmp(i) = strcat(sample,'\',folnameCrop);
    else
        if(strcmp(done,cell2mat(myList{i,CropColumn})))
            LoadTmp(i) = strcat(sample,'\',folnameCrop);
        end
    end
    
    LogProcess(logfile,'Progressbar',num2str(i),num2str(Ng));
end


%Mask
LogProcess(logfile,'Operation','Creating Mask');
LogProcess(logfile,'SampleName','');
LogProcess(logfile,'Progressbar',num2str(0),num2str(Ng));

for i=1:Ng
    sample = cell2mat(myList{i,SampleColumn});
    fname = br_name(sample);
    mkdir(sample,folnameMask);
    
    if(strcmp(yes,cell2mat(myList{i,MaskColumn})))
        LogProcess(logfile,'SampleName',sample);
        IM=gui_Rload(strcat(LoadTmp(i),'\*.tif'), strcat(LoadTmp(i),'\') ,1.6,1.6);
        OUT=Vmask( IM, midlvl );
        for j=1:size(OUT,3)
            imwrite(uint16(OUT(:,:,j)),strcat(sample,'\',folnameMask,'\',fname,'_', sprintf('%06d.tif',j)));
        end
    end
    LogProcess(logfile,'Progressbar',num2str(i),num2str(Ng));
end    

end

%------------------------------------
function name = br_name(samplename)
    name=samplename;
    st=strfind(name,'\');
    name=name(st(end)+1:end); 
end