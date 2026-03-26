# Backend Test - MyOffice ACPD API

## 技術
- .NET 8 Web API
- ADO.NET
- SQL Server

## 功能
- CRUD API for MyOffice_ACPD

## 執行方式

### 1. 還原資料庫
使用 SQL Server 還原 `Myoffice.bak`

### 2. 修改連線字串

在 appsettings.json：


"ConnectionStrings": {
"DefaultConnection": "Server=localhost;Database=Myoffice;Trusted_Connection=True;TrustServerCertificate=True;"
}


### 3. 執行專案
使用 Visual Studio 2022 開啟專案並按 F5

### 4. Swagger 測試
啟動後會自動開啟 Swagger UI，可測試所有 API

## API

- GET /api/myofficeacpd
- GET /api/myofficeacpd/{id}
- POST /api/myofficeacpd
- PUT /api/myofficeacpd/{id}
- DELETE /api/myofficeacpd/{id}

### 5. 測試資料
{
  "acpD_Cname": "王小明",
  "acpD_Ename": "David Wang",
  "acpD_Sname": "小明",
  "acpD_Email": "test@gmail.com",
  "acpD_Status": 1,
  "acpD_Stop": false,
  "acpD_StopMemo": "正常",
  "acpD_LoginID": "test001",
  "acpD_LoginPWD": "123456",
  "acpD_Memo": "測試資料"
}
