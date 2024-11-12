
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
DROP TABLE [dbo].[Users]
GO

CREATE TABLE dbo.Users (
    UserId INT PRIMARY KEY IDENTITY,
    Fullname NVARCHAR(50) UNIQUE NOT NULL,
	EmailId NVARCHAR(50) UNIQUE NOT NULL,
    PasswordHash NVARCHAR(500) NOT NULL,
	MobileNo Nvarchar(20) NULL,
	City NVarchar(100) NULL,
	UserRole INT NOT NULL,
    CreatedDateTime DATETIME NOT NULL DEFAULT GETDATE(),
    UpdatedDateTime DATETIME NULL,
	CreatedBy int NULL,
	UpdatedBy int NULL,
    IsActive BIT NOT NULL DEFAULT 1
);
Go

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuditTrails]') AND type in (N'U'))
DROP TABLE [dbo].AuditTrails
GO
CREATE TABLE AuditTrails (
    AuditId INT PRIMARY KEY IDENTITY,
    [ActionName] NVARCHAR(50),
    UserId INT,
    [Timestamp] DATETIME DEFAULT GETDATE()
);
GO

INSERT INTO Users(Fullname,EmailId,PasswordHash, MobileNo,City, UserRole, CreatedDateTime,IsActive)
VALUES('one','one@s.com','Y5O9dWbFtcNdzRuqK6nfchf2ezF+M10q0ZjtL2X9t3joef9QN+uGEkRNdrI2QI4K','44313835434','UAE',3,getdate(),1)
, ('two','two@s.com','Y5O9dWbFtcNdzRuqK6nfchf2ezF+M10q0ZjtL2X9t3joef9QN+uGEkRNdrI2QI4K','44313835434','UAE',3,getdate(),1),
('three','three@s.com','Y5O9dWbFtcNdzRuqK6nfchf2ezF+M10q0ZjtL2X9t3joef9QN+uGEkRNdrI2QI4K','44313835434','UAE',3,getdate(),1),
('four','four@s.com','Y5O9dWbFtcNdzRuqK6nfchf2ezF+M10q0ZjtL2X9t3joef9QN+uGEkRNdrI2QI4K','44313835434','UAE',3,getdate(),1),
('five','five@s.com','Y5O9dWbFtcNdzRuqK6nfchf2ezF+M10q0ZjtL2X9t3joef9QN+uGEkRNdrI2QI4K','44313835434','UAE',3,getdate(),1)
, ('six','six@s.com','Y5O9dWbFtcNdzRuqK6nfchf2ezF+M10q0ZjtL2X9t3joef9QN+uGEkRNdrI2QI4K','44313835434','UAE',3,getdate(),1),
('seven','seven@s.com','Y5O9dWbFtcNdzRuqK6nfchf2ezF+M10q0ZjtL2X9t3joef9QN+uGEkRNdrI2QI4K','44313835434','UAE',3,getdate(),1),
('eight','eight@s.com','Y5O9dWbFtcNdzRuqK6nfchf2ezF+M10q0ZjtL2X9t3joef9QN+uGEkRNdrI2QI4K','44313835434','UAE',3,getdate(),1),
('nine','nine@s.com','Y5O9dWbFtcNdzRuqK6nfchf2ezF+M10q0ZjtL2X9t3joef9QN+uGEkRNdrI2QI4K','44313835434','UAE',3,getdate(),1)
, ('ten','ten@s.com','Y5O9dWbFtcNdzRuqK6nfchf2ezF+M10q0ZjtL2X9t3joef9QN+uGEkRNdrI2QI4K','44313835434','UAE',3,getdate(),1),
('one1','one1@s.com','Y5O9dWbFtcNdzRuqK6nfchf2ezF+M10q0ZjtL2X9t3joef9QN+uGEkRNdrI2QI4K','44313835434','UAE',3,getdate(),1),
('two1','two1@s.com','Y5O9dWbFtcNdzRuqK6nfchf2ezF+M10q0ZjtL2X9t3joef9QN+uGEkRNdrI2QI4K','44313835434','UAE',3,getdate(),1)