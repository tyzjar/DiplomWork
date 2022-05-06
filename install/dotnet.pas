//-----------------------------------------------------------------------------
//  �������� ������� ������� ����������
//-----------------------------------------------------------------------------
function IsDotNetDetected(release: cardinal): boolean;

var 
    reg_key: string; // ��������������� ��������� ���������� �������
    success: boolean; // ���� ������� ������������� ������ .NET
    release45: cardinal; // ����� ������ ��� ������ 4.5.x
    key_value: cardinal; // ����������� �� ������� �������� �����
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
//  �������-������� ��� �������������� ���������� ������ ��� ������
//-----------------------------------------------------------------------------
function IsRequiredDotNetDetected(): boolean;
begin
    result := IsDotNetDetected(461808);
end;

//-----------------------------------------------------------------------------
//    Callback-�������, ���������� ��� ������������� ���������
//-----------------------------------------------------------------------------
function InitializeSetup(): boolean;
begin

  // ���� ��� ��������� ������ .NET ������� ��������� � ���, ��� �����������
  // ���������� ���������� � �� ������ ���������
  if not IsDotNetDetected(461808) then
    begin
      MsgBox('{#ProjectName} requires Microsoft .NET Framework 4.7.2 Full Profile.'#13#13
             'The installer will attempt to install it', mbInformation, MB_OK);
    end;   

  result := true;
end;