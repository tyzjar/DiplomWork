function gui_segmentscellcount(logfile, expname, atlasname)
    

myList = table2cell(readtable(expname));

%Number of samples
Ng = size(myList,1);

LogProcess(logfile,'Operation','Segments count');
LogProcess(logfile,'SampleName','');
LogProcess(logfile,'Progressbar',num2str(0),num2str(1));

atlasname = strcat(atlasname,'\*.tif');
atl = Rload(atlasname, 1, 1);

for u=1:Ng
    
    LogProcess(logfile,'SampleName',myList{u,1});
    tmp = myList{u,1};
    chdir(tmp);

    B = readtable('cells.txt');
    
    sizeB = size(B,1);
    id = zeros([sizeB,1]);
    X = zeros([sizeB,1]);
    Y = zeros([sizeB,1]);
    Z = zeros([sizeB,1]);
    
    for i=1:sizeB
      id(i) = atl(B.X(i),B.Y(i),B.Z(i));
      X(i) = B.X(i);
      Y(i) = B.Y(i);
      Z(i) = B.Z(i);
    end
    
    T = table(X, Y, Z, id,'VariableNames',{'X','Y','Z','Id'});
    writetable(T,sprintf('cells_segments.txt'));
    LogProcess(logfile,'Progressbar',num2str(u),num2str(Ng));
end %end u:Ng

end