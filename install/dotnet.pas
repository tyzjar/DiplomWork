//-----------------------------------------------------------------------------
//  Проверка наличия нужного фреймворка
//-----------------------------------------------------------------------------
function IsDotNetDetected(release: cardinal): boolean;

var 
    reg_key: string; // Просматриваемый подраздел системного реестра
    success: boolean; // Флаг наличия запрашиваемой версии .NET
    release45: cardinal; // Номер релиза для версии 4.5.x
    key_value: cardinal; // Прочитанное из реестра значение ключа
    sub_key: string;

begin
   success := false;
   reg_key := 'SOFTWARE\Microsoft\NET Framework Setup\NDP\';
 
   sub_key := 'v4\Full';
   reg_key := reg_key + sub_key;
   success := RegQueryDWordValue(HKLM, reg_key, 'Release', release45);
   success := success and (release45 >= release);   
   result := success;
end;

//-----------------------------------------------------------------------------
//  Функция-обертка для детектирования конкретной нужной нам версии
//-----------------------------------------------------------------------------
function IsRequiredDotNetDetected(): boolean;
begin
    result := IsDotNetDetected(461808);
end;

//-----------------------------------------------------------------------------
//    Callback-функция, вызываемая при инициализации установки
//-----------------------------------------------------------------------------
function InitializeSetup(): boolean;
begin

  // Если нет тербуемой версии .NET выводим сообщение о том, что инсталлятор
  // попытается установить её на данный компьютер
  if not IsDotNetDetected(461808) then
    begin
      MsgBox('{#ProjectName} requires Microsoft .NET Framework 4.7.2 Full Profile.'#13#13
             'The installer will attempt to install it', mbInformation, MB_OK);
    end;   

  result := true;
end;