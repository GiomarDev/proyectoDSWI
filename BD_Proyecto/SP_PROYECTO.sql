USE COMPULAST
GO

--SP LOGIN
IF OBJECT_ID('SP_BUSCAR_USUARIO') IS NOT NULL 
	DROP PROC SP_BUSCAR_USUARIO
GO

CREATE OR ALTER PROCEDURE SP_BUSCAR_USUARIO
@p_usuari_codigo varchar(8),
@p_usuari_pass varchar(10)
AS
BEGIN
	SELECT USUARI_CODIGO, USUARI_PASS, USUARI_APEPAT, USUARI_APEMAT, USUARI_NOMBRES, USUARI_CORREO, USUARI_TELEFONO
	FROM [dbo].[TBUSUARIO]
	WHERE USUARI_CODIGO = @p_usuari_codigo and USUARI_PASS = @p_usuari_pass
END
GO
--exec SP_BUSCAR_USUARIO 1001,'giomar'


--SP PC
IF OBJECT_ID('SP_LISTAPC')IS NOT NULL 
	DROP PROC SP_LISTAPC
GO
CREATE PROC SP_LISTAPC
AS
	SELECT P.ID_PC, P.DES_PC, PR.NOM_PROC, RM.DES_RAM, AL.DES_ALM,FU.DES_PSU,P.PRE_PC, P.STK_PC, P.FOT_PC
		FROM TB_PC P
		JOIN TB_PROCESADOR PR ON P.ID_PROC=PR.ID_PROC
		JOIN TB_RAM RM ON P.ID_RAM=RM.ID_RAM
		JOIN TB_ALMACENAMIENTO AL ON P.ID_ALM=AL.ID_ALM
		JOIN TB_FUENTE FU ON P.ID_PSU=FU.ID_PSU
GO
--select * from TB_PC


--SP PC ORIGINAL(REGISTRO, EDIT)
IF OBJECT_ID('SP_LISTAPCORIGINAL')IS NOT NULL 
	DROP PROC SP_LISTAPCORIGINAL
GO
CREATE PROC SP_LISTAPCORIGINAL
AS
	SELECT P.* FROM TB_PC P
GO