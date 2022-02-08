function LogProcess (logfile, varName, varargin)

delimiter1 = '@';
delimiter2 = ';';

file=fopen(logfile,'a');
str = strcat(varName,delimiter1,strjoin(varargin,delimiter2));
fprintf(file,'%s\n',str);
fclose(file);

end