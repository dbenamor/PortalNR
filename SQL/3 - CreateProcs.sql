USE [PORTALNR]
GO
/****** Object:  StoredProcedure [dbo].[SP_ATUALIZA_FUNCIONARIOS_MTR]    Script Date: 15/05/2023 23:48:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--EXEC SP_ATUALIZA_FUNCIONARIOS_MTR

CREATE PROCEDURE [dbo].[SP_ATUALIZA_FUNCIONARIOS_MTR]
AS
BEGIN

TRUNCATE TABLE TB_FUNCIONARIOS_MTR

INSERT INTO TB_FUNCIONARIOS_MTR

SELECT DISTINCT 
       [EMCODEMP]
      ,[EMPRESA]
      ,[MATRICULA]
      ,[FUNCIONARIO]
      ,[CARGO]
	  ,[COORDENACAO] = '-'
      ,[GERENCIA] = '-'
	  ,[CENTRO_CUSTO]
      ,[DESC_CENTRO_CUSTO]
      ,[SITUACAO]	  
      ,[COD_GH]      
	  ,[DESC_GH]
      ,[DTA_SITUACAO]
      ,[DTA_ADMISSAO]
	  ,[EMAIL]  
	  ,GETDATE() AS [DTA_ATUALIZACAO]
	 FROM [dbo].[vw_funcionarios_mtr]
	 ORDER BY [FUNCIONARIO]

 END

 BEGIN
	INSERT INTO TB_LOG
	SELECT TOP 1 @@ROWCOUNT
		,[DTA_ATUALIZACAO]
	FROM TB_FUNCIONARIOS_MTR

	END
 

GO
/****** Object:  StoredProcedure [dbo].[sp_ATUALIZASITUACAOEFETIVO]    Script Date: 15/05/2023 23:48:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[sp_ATUALIZASITUACAOEFETIVO] AS

DECLARE
@MATRICULA INT,
@SITUACAOEFETIVO NVARCHAR(MAX)


/***********************************************************************/        
DECLARE CUR_SITUEFETIVO CURSOR LOCAL FAST_FORWARD FOR    
SELECT DISTINCT [MATRICULA], [SITUACAO] FROM TB_FUNCIONARIOS_MTR WHERE [EMPRESA] = 'CONCESSÃO METROVIÁRIA DO RIO DE JANEIRO S.A'
 
OPEN CUR_SITUEFETIVO;

FETCH NEXT FROM CUR_SITUEFETIVO INTO @MATRICULA, @SITUACAOEFETIVO

	WHILE @@FETCH_STATUS = 0
BEGIN
	SET NOCOUNT ON; 

UPDATE TB_COLABORADOR SET [SITUACAO] = 
CASE 
WHEN @SITUACAOEFETIVO = 'AFAST ACID TRAB <= 15 DIAS'		THEN 'AFASTADO < = 15 DIAS'
WHEN @SITUACAOEFETIVO = 'AFAST ACID TRAB > 15 DIAS'			THEN 'AFASTADO > 15 DIAS'
WHEN @SITUACAOEFETIVO = 'AFAST APOSENT EXCETO DOE/AC TR'	THEN 'DESLIGADO'
WHEN @SITUACAOEFETIVO = 'AFAST DOENCA <= 15 DIAS'			THEN 'AFASTADO < = 15 DIAS'
WHEN @SITUACAOEFETIVO = 'AFAST DOENCA <= 15 DIAS COVID'		THEN 'AFASTADO < = 15 DIAS'
WHEN @SITUACAOEFETIVO = 'AFAST  > 15 DIAS'					THEN 'AFASTADO > 15 DIAS'
WHEN @SITUACAOEFETIVO = 'AFAST LICENCA MATERNIDADE'			THEN 'AFASTADO > 15 DIAS'
WHEN @SITUACAOEFETIVO = 'AFAST RECURSO INSS'				THEN 'AFASTADO > 15 DIAS'
WHEN @SITUACAOEFETIVO = 'AFAST SUSPENSÃO TEMP MP 936'		THEN 'AFASTADO > 15 DIAS'
WHEN @SITUACAOEFETIVO = 'APOSENT INVALIDEZ AC TRAB'			THEN 'DESLIGADO'
WHEN @SITUACAOEFETIVO = 'ATIVIDADE NORMAL'					THEN 'ATIVIDADE NORMAL'
WHEN @SITUACAOEFETIVO = 'ESTAGIARIO'						THEN 'ESTAGIARIO'
WHEN @SITUACAOEFETIVO = 'FERIAS NOR AD 13 SAL/AB PECUN'		THEN 'FÉRIAS'
WHEN @SITUACAOEFETIVO = 'FERIAS NOR AD 13 SALARIO'			THEN 'FÉRIAS'
WHEN @SITUACAOEFETIVO = 'FERIAS NOR COM AB PECUNIARIO'		THEN 'FÉRIAS'
WHEN @SITUACAOEFETIVO = 'FERIAS NORMAIS'					THEN 'FÉRIAS'
--WHEN @SITUACAOEFETIVO = 'PRO LABORE'						THEN 'OUTROS'
WHEN @SITUACAOEFETIVO = 'RECESSO ESTAGIARIO'				THEN 'RECESSO'
WHEN @SITUACAOEFETIVO = 'RESC APOSENTA.POR INVALIDEZ'		THEN 'DESLIGADO'
WHEN @SITUACAOEFETIVO = 'RESC COMUM ACORDO'					THEN 'DESLIGADO'
WHEN @SITUACAOEFETIVO = 'RESC DEM TERMINO CONTRATO'			THEN 'DESLIGADO'
WHEN @SITUACAOEFETIVO = 'RESC DEMIS COM JUSTA CAUSA'		THEN 'DESLIGADO'
WHEN @SITUACAOEFETIVO = 'RESC DEMIS NA EXPERIENCIA'			THEN 'DESLIGADO'
WHEN @SITUACAOEFETIVO = 'RESC DEMIS S/ JC AVISO IND'		THEN 'DESLIGADO'
WHEN @SITUACAOEFETIVO = 'RESC FALECIMENTO'					THEN 'DESLIGADO'
WHEN @SITUACAOEFETIVO = 'RESC PEDIDO DEM ANTES TERM CON'	THEN 'DESLIGADO'
WHEN @SITUACAOEFETIVO = 'RESC PEDIDO DEMISS AVISO IND'		THEN 'DESLIGADO'
WHEN @SITUACAOEFETIVO = 'RESC PEDIDO DEMISS AVISO TRAB'		THEN 'DESLIGADO'
WHEN @SITUACAOEFETIVO = 'RESC PEDIDO DEMISS NA EXP'			THEN 'DESLIGADO'
WHEN @SITUACAOEFETIVO = 'RESC TER CON NA EXPERIENCIA'		THEN 'DESLIGADO'
WHEN @SITUACAOEFETIVO = 'RESC TER CONT ANTECIPADO'			THEN 'DESLIGADO'
WHEN @SITUACAOEFETIVO = 'RESC TER CONT NO PRAZO'			THEN 'DESLIGADO'
--WHEN @SITUACAOEFETIVO = 'SUSP CONTRATO ELEICAO DIRETOR'		THEN 'OUTROS'
--WHEN @SITUACAOEFETIVO = 'SUSPENSAO CONTRATO TRABALHO'		THEN 'OUTROS'
WHEN @SITUACAOEFETIVO = 'TERMINO ESTAGIO'					THEN 'DESLIGADO'
ELSE 'OUTROS' END
WHERE [MATRICULA] = @MATRICULA 


FETCH NEXT FROM CUR_SITUEFETIVO INTO @MATRICULA, @SITUACAOEFETIVO ;
END
CLOSE CUR_SITUEFETIVO;
DEALLOCATE CUR_SITUEFETIVO;


GO
/****** Object:  StoredProcedure [dbo].[sp_AVISO_NR_A_VENCER_90DIAS]    Script Date: 15/05/2023 23:48:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_AVISO_NR_A_VENCER_90DIAS] AS 
DECLARE 
@VENCIMENTO INT = 30,
@assunto    VARCHAR(200), 
@CorpoEmail NVARCHAR(MAX),
@comunicadoHTML NVARCHAR(MAX),
@NORMATIVO VARCHAR(10) = 'NR-10',
@NORMATIVO1 VARCHAR(10) = 'NR-10 SEP',
@NORMATIVO2 VARCHAR(10) = 'NR-12',
@NORMATIVO3 VARCHAR(10) = 'NR-33',
@NORMATIVO4 VARCHAR(10) = 'NR-35',
@NORMATIVO5 VARCHAR(10) = 'REG-55',
@HTML NVARCHAR(MAX),
@HTML1 NVARCHAR(MAX),
@HTML2 NVARCHAR(MAX),
@HTML3 NVARCHAR(MAX),
@HTML4 NVARCHAR(MAX),
@HTML5 NVARCHAR(MAX),
@EMAIL NVARCHAR(MAX),
@VALIDADOR INT,
@VALIDADOR1 INT,
@VALIDADOR2 INT,
@VALIDADOR3 INT,
@VALIDADOR4 INT,
@VALIDADOR5 INT


SET @comunicadoHTML = N'<html>
	                    <head><title></title></head>
						<body>
						    <font face="Tahoma" size="2" color="#004A8F"><p>Prezados(as) 
						    <br /><br />Seguem abaixo os colaboradores com treinamento normativo expirado ou a vencer em até ' + CONVERT(NVARCHAR,@VENCIMENTO) + ' dias.
							<br /><br />Qualquer dúvida entrar em contato pelo e-mail 
								<a href="mailto:controlenormativos@gmail.com.br?Subject=Colaboradores expirados ou a vencer">@Controle Normativos.</a>
						    </p></font>
						</body></html>'

/*************************************************************************/
/*    CURSOR PARA PARA PEGAR EMAIL DA COORDENAÇÃO PARA ENVIAR O EMAIL    */
/*************************************************************************/        
    DECLARE CUR_EMAIL CURSOR LOCAL FAST_FORWARD FOR    
	SELECT DISTINCT [EMAIL] FROM vwEMAILSCOORDENACAO WHERE [EMAIL] IS NOT NULL
	 
    OPEN CUR_EMAIL;
 
    FETCH NEXT FROM CUR_EMAIL INTO @EMAIL;
	--SET @HTML = ''
		WHILE @@FETCH_STATUS = 0
    BEGIN
		SET NOCOUNT ON; 

/*************************************************************************/
/*                            CONSULTA NR-10                             */
/*************************************************************************/ 
SET @Validador = ( SELECT DISTINCT COUNT(A.[VENCIMENTO]) 
				   FROM [vwCOLABORADOR] A
				   INNER JOIN [TB_EMAILCOORDENACAO] EMAIL ON A.[COORDENACAO] = EMAIL.[COORDENACAO]
				   WHERE A.[NORMATIVO] = @NORMATIVO 
				   AND A.[VENCIMENTO] <= @VENCIMENTO AND EMAIL.[EMAIL] = @EMAIL
				   AND A.[SITUACAO] IN ('ATIVIDADE NORMAL','FÉRIAS','ESTAGIÁRIO','AFASTADO < = 15 DIAS')
				 )

IF @validador > 0

SET @HTML =
	      N'<html>
			<head>
			<title></title>
				<style type="text/css">
					table { border: 1px; font-family: tahoma; font-size: 12px; }    
					th { background: #e6e6e6; padding-right: 10px; padding-left: 10px; }
					tr { text-align: center; }
					td { padding-right: 10px; padding-left: 10px; }
				</style>
			</head>	
			<body>						
			<hr />					
			<h3>' + @NORMATIVO + ' À VENCER</h3>
			<table>
				<thead>
				  <tr style="text-align: center;">
				      <th>MATRÍCULA</th>
			          <th>NOME</th>
			          <th>COORDENAÇÃO</th>
					  <th>E-MAIL</th>
			          <th>SITUAÇÃO</th>
			          <th>DATA DE CERTIFICAÇÃO</th>
			          <th>DATA DE REVALIDAÇÃO</th>
			          <th>VIGÊNCIA</th>
			          <th>VENCIMENTO EM (DIAS)</th>
				  </tr>
				</thead>    
		    <tbody>' +
	CAST( (SELECT
		 td = NR.[MATRICULA]	, ''
		,td = NR.[NOME]			, ''
		,td = NR.[COORDENACAO]	, ''
		,td = EMAIL.[EMAIL]	    , ''
		,td = NR.[SITUACAO]     , ''
		,td = ISNULL(CONVERT(VARCHAR,NR.[DTCERTIFICACAO],103),'') , ''
		,td = ISNULL(CONVERT(VARCHAR,NR.[DTREVALIDACAO],103),'')  , ''
		,td = ISNULL(CONVERT(VARCHAR,NR.[DTVIGENCIA],103),'')	  , ''
		,td = NR.[VENCIMENTO] , ''
	FROM [vwCOLABORADOR] NR 
		INNER JOIN [TB_EMAILCOORDENACAO] EMAIL ON NR.[COORDENACAO] = EMAIL.[COORDENACAO]  
	WHERE NR.[NORMATIVO] = @NORMATIVO 
		AND NR.[SITUACAO] IN ('ATIVIDADE NORMAL','FÉRIAS','ESTAGIÁRIO','AFASTADO < = 15 DIAS')
		AND NR.[VENCIMENTO] <= @VENCIMENTO
		--AND  EMAIL.[COORDENACAO] = @COORDENACAO
		AND EMAIL.[EMAIL] = @EMAIL
		ORDER BY NR.[COORDENACAO], NR.[MATRICULA]
				FOR XML PATH('tr'), TYPE) AS NVARCHAR(MAX) ) +
			'</toby></table></body></html>'	

ELSE

 SET @HTML = '<hr /><br /><font face="Tahoma" size="2" color ="#004A8F"><strong>NÃO HÁ ' + @NORMATIVO + ' PARA VENCER EM ' + CONVERT(NVARCHAR,@VENCIMENTO) + ' DIAS OU MENOS.</strong></font><br /><br />'

/*************************************************************************/
/*                        CONSULTA NR-10 SEP                             */
/*************************************************************************/ 
SET @VALIDADOR1 = ( SELECT DISTINCT COUNT(A.[VENCIMENTO]) 
				   FROM [vwCOLABORADOR] A
				   INNER JOIN [TB_EMAILCOORDENACAO] EMAIL ON A.[COORDENACAO] = EMAIL.[COORDENACAO]
				   WHERE A.[NORMATIVO] = @NORMATIVO1 
				   AND A.[VENCIMENTO] <= @VENCIMENTO AND EMAIL.[EMAIL] = @EMAIL
				   AND A.[SITUACAO] IN ('ATIVIDADE NORMAL','FÉRIAS','ESTAGIÁRIO','AFASTADO < = 15 DIAS')
				 )

IF @VALIDADOR1 > 0

SET @HTML1 =
	      N'<html>
			<head>
			<title></title>
				<style type="text/css">
					table { border: 1px; font-family: tahoma; font-size: 12px; }    
					th { background: #e6e6e6; padding-right: 10px; padding-left: 10px; }
					tr { text-align: center; }
					td { padding-right: 10px; padding-left: 10px; }
				</style>
			</head>	
			<body>						
			<hr />					
			<h3>' + @NORMATIVO1 + ' À VENCER</h3>
			<table>
				<thead>
				  <tr style="text-align: center;">
				      <th>MATRÍCULA</th>
			          <th>NOME</th>
			          <th>COORDENAÇÃO</th>
					  <th>E-MAIL</th>
			          <th>SITUAÇÃO</th>
			          <th>DATA DE CERTIFICAÇÃO</th>
			          <th>DATA DE REVALIDAÇÃO</th>
			          <th>VIGÊNCIA</th>
			          <th>VENCIMENTO EM (DIAS)</th>
				  </tr>
				</thead>    
		    <tbody>' +
	CAST( (SELECT
		 td = NR.[MATRICULA]	, ''
		,td = NR.[NOME]			, ''
		,td = NR.[COORDENACAO]	, ''
		,td = EMAIL.[EMAIL]	    , ''
		,td = NR.[SITUACAO]     , ''
		,td = ISNULL(CONVERT(VARCHAR,NR.[DTCERTIFICACAO],103),'') , ''
		,td = ISNULL(CONVERT(VARCHAR,NR.[DTREVALIDACAO],103),'')  , ''
		,td = ISNULL(CONVERT(VARCHAR,NR.[DTVIGENCIA],103),'')	  , ''
		,td = NR.[VENCIMENTO] , ''
	FROM [vwCOLABORADOR] NR 
		INNER JOIN [TB_EMAILCOORDENACAO] EMAIL ON NR.[COORDENACAO] = EMAIL.[COORDENACAO]  
	WHERE NR.[NORMATIVO] = @NORMATIVO1 
		AND NR.[SITUACAO] IN ('ATIVIDADE NORMAL','FÉRIAS','ESTAGIÁRIO','AFASTADO < = 15 DIAS')
		AND NR.[VENCIMENTO] <= @VENCIMENTO
		--AND  EMAIL.[COORDENACAO] = @COORDENACAO
		AND EMAIL.[EMAIL] = @EMAIL
		ORDER BY NR.[COORDENACAO], NR.[MATRICULA]
				FOR XML PATH('tr'), TYPE) AS NVARCHAR(MAX) ) +
			'</toby></table></body></html>'	

ELSE

 SET @HTML1 = '<hr /><br /><font face="Tahoma" size="2" color ="#004A8F"><strong>NÃO HÁ ' + @NORMATIVO1 + ' PARA VENCER EM ' + CONVERT(NVARCHAR,@VENCIMENTO) + ' DIAS OU MENOS.</strong></font><br /><br />'

/*************************************************************************/
/*                            CONSULTA NR-12                             */
/*************************************************************************/ 
SET @VALIDADOR2 = ( SELECT DISTINCT COUNT(A.[VENCIMENTO]) 
				   FROM [vwCOLABORADOR] A
				   INNER JOIN [TB_EMAILCOORDENACAO] EMAIL ON A.[COORDENACAO] = EMAIL.[COORDENACAO]
				   WHERE A.[NORMATIVO] = @NORMATIVO2 
				   AND A.[VENCIMENTO] <= @VENCIMENTO AND EMAIL.[EMAIL] = @EMAIL
				   AND A.[SITUACAO] IN ('ATIVIDADE NORMAL','FÉRIAS','ESTAGIÁRIO','AFASTADO < = 15 DIAS')
				 )

IF @VALIDADOR2 > 0

SET @HTML2 =
	      N'<html>
			<head>
			<title></title>
				<style type="text/css">
					table { border: 1px; font-family: tahoma; font-size: 12px; }    
					th { background: #e6e6e6; padding-right: 10px; padding-left: 10px; }
					tr { text-align: center; }
					td { padding-right: 10px; padding-left: 10px; }
				</style>
			</head>	
			<body>						
			<hr />					
			<h3>' + @NORMATIVO2 + ' À VENCER</h3>
			<table>
				<thead>
				  <tr style="text-align: center;">
				      <th>MATRÍCULA</th>
			          <th>NOME</th>
			          <th>COORDENAÇÃO</th>
					  <th>E-MAIL</th>
			          <th>SITUAÇÃO</th>
			          <th>DATA DE CERTIFICAÇÃO</th>
			          <th>DATA DE REVALIDAÇÃO</th>
			          <th>VIGÊNCIA</th>
			          <th>VENCIMENTO EM (DIAS)</th>
				  </tr>
				</thead>    
		    <tbody>' +
	CAST( (SELECT
		 td = NR.[MATRICULA]	, ''
		,td = NR.[NOME]			, ''
		,td = NR.[COORDENACAO]	, ''
		,td = EMAIL.[EMAIL]	    , ''
		,td = NR.[SITUACAO]     , ''
		,td = ISNULL(CONVERT(VARCHAR,NR.[DTCERTIFICACAO],103),'') , ''
		,td = ISNULL(CONVERT(VARCHAR,NR.[DTREVALIDACAO],103),'')  , ''
		,td = ISNULL(CONVERT(VARCHAR,NR.[DTVIGENCIA],103),'')	  , ''
		,td = NR.[VENCIMENTO] , ''
	FROM [vwCOLABORADOR] NR 
		INNER JOIN [TB_EMAILCOORDENACAO] EMAIL ON NR.[COORDENACAO] = EMAIL.[COORDENACAO]  
	WHERE NR.[NORMATIVO] = @NORMATIVO2 
		AND NR.[SITUACAO] IN ('ATIVIDADE NORMAL','FÉRIAS','ESTAGIÁRIO','AFASTADO < = 15 DIAS')
		AND NR.[VENCIMENTO] <= @VENCIMENTO
		--AND  EMAIL.[COORDENACAO] = @COORDENACAO
		AND EMAIL.[EMAIL] = @EMAIL
		ORDER BY NR.[COORDENACAO], NR.[MATRICULA]
				FOR XML PATH('tr'), TYPE) AS NVARCHAR(MAX) ) +
			'</toby></table></body></html>'	

ELSE

 SET @HTML2 = '<hr /><br /><font face="Tahoma" size="2" color ="#004A8F"><strong>NÃO HÁ ' + @NORMATIVO2 + ' PARA VENCER EM ' + CONVERT(NVARCHAR,@VENCIMENTO) + ' DIAS OU MENOS.</strong></font><br /><br />'

/*************************************************************************/
/*                            CONSULTA NR-33                             */
/*************************************************************************/ 
SET @VALIDADOR3 = ( SELECT DISTINCT COUNT(A.[VENCIMENTO]) 
				   FROM [vwCOLABORADOR] A
				   INNER JOIN [TB_EMAILCOORDENACAO] EMAIL ON A.[COORDENACAO] = EMAIL.[COORDENACAO]
				   WHERE A.[NORMATIVO] = @NORMATIVO3 
				   AND A.[VENCIMENTO] <= @VENCIMENTO AND EMAIL.[EMAIL] = @EMAIL
				   AND A.[SITUACAO] IN ('ATIVIDADE NORMAL','FÉRIAS','ESTAGIÁRIO','AFASTADO < = 15 DIAS')
				 )

IF @VALIDADOR3 > 0

SET @HTML3 =
	      N'<html>
			<head>
			<title></title>
				<style type="text/css">
					table { border: 1px; font-family: tahoma; font-size: 12px; }    
					th { background: #e6e6e6; padding-right: 10px; padding-left: 10px; }
					tr { text-align: center; }
					td { padding-right: 10px; padding-left: 10px; }
				</style>
			</head>	
			<body>						
			<hr />					
			<h3>' + @NORMATIVO3 + ' À VENCER</h3>
			<table>
				<thead>
				  <tr style="text-align: center;">
				      <th>MATRÍCULA</th>
			          <th>NOME</th>
			          <th>COORDENAÇÃO</th>
					  <th>E-MAIL</th>
			          <th>SITUAÇÃO</th>
			          <th>DATA DE CERTIFICAÇÃO</th>
			          <th>DATA DE REVALIDAÇÃO</th>
			          <th>VIGÊNCIA</th>
			          <th>VENCIMENTO EM (DIAS)</th>
				  </tr>
				</thead>    
		    <tbody>' +
	CAST( (SELECT
		 td = NR.[MATRICULA]	, ''
		,td = NR.[NOME]			, ''
		,td = NR.[COORDENACAO]	, ''
		,td = EMAIL.[EMAIL]	    , ''
		,td = NR.[SITUACAO]     , ''
		,td = ISNULL(CONVERT(VARCHAR,NR.[DTCERTIFICACAO],103),'') , ''
		,td = ISNULL(CONVERT(VARCHAR,NR.[DTREVALIDACAO],103),'')  , ''
		,td = ISNULL(CONVERT(VARCHAR,NR.[DTVIGENCIA],103),'')	  , ''
		,td = NR.[VENCIMENTO] , ''
	FROM [vwCOLABORADOR] NR 
		INNER JOIN [TB_EMAILCOORDENACAO] EMAIL ON NR.[COORDENACAO] = EMAIL.[COORDENACAO]  
	WHERE NR.[NORMATIVO] = @NORMATIVO3 
		AND NR.[SITUACAO] IN ('ATIVIDADE NORMAL','FÉRIAS','ESTAGIÁRIO','AFASTADO < = 15 DIAS')
		AND NR.[VENCIMENTO] <= @VENCIMENTO
		--AND  EMAIL.[COORDENACAO] = @COORDENACAO
		AND EMAIL.[EMAIL] = @EMAIL
		ORDER BY NR.[COORDENACAO], NR.[MATRICULA]
				FOR XML PATH('tr'), TYPE) AS NVARCHAR(MAX) ) +
			'</toby></table></body></html>'	

ELSE

 SET @HTML3 = '<hr /><br /><font face="Tahoma" size="2" color ="#004A8F"><strong>NÃO HÁ ' + @NORMATIVO3 + ' PARA VENCER EM ' + CONVERT(NVARCHAR,@VENCIMENTO) + ' DIAS OU MENOS.</strong></font><br /><br />'

/*************************************************************************/
/*                            CONSULTA NR-35                             */
/*************************************************************************/ 
SET @VALIDADOR4 = ( SELECT DISTINCT COUNT(A.[VENCIMENTO]) 
				   FROM [vwCOLABORADOR] A
				   INNER JOIN [TB_EMAILCOORDENACAO] EMAIL ON A.[COORDENACAO] = EMAIL.[COORDENACAO]
				   WHERE A.[NORMATIVO] = @NORMATIVO4 
				   AND A.[VENCIMENTO] <= @VENCIMENTO AND EMAIL.[EMAIL] = @EMAIL
				   AND A.[SITUACAO] IN ('ATIVIDADE NORMAL','FÉRIAS','ESTAGIÁRIO','AFASTADO < = 15 DIAS')
				 )

IF @VALIDADOR4 > 0

SET @HTML4 =
	      N'<html>
			<head>
			<title></title>
				<style type="text/css">
					table { border: 1px; font-family: tahoma; font-size: 12px; }    
					th { background: #e6e6e6; padding-right: 10px; padding-left: 10px; }
					tr { text-align: center; }
					td { padding-right: 10px; padding-left: 10px; }
				</style>
			</head>	
			<body>						
			<hr />					
			<h3>' + @NORMATIVO4 + ' À VENCER</h3>
			<table>
				<thead>
				  <tr style="text-align: center;">
				      <th>MATRÍCULA</th>
			          <th>NOME</th>
			          <th>COORDENAÇÃO</th>
					  <th>E-MAIL</th>
			          <th>SITUAÇÃO</th>
			          <th>DATA DE CERTIFICAÇÃO</th>
			          <th>DATA DE REVALIDAÇÃO</th>
			          <th>VIGÊNCIA</th>
			          <th>VENCIMENTO EM (DIAS)</th>
				  </tr>
				</thead>    
		    <tbody>' +
	CAST( (SELECT
		 td = NR.[MATRICULA]	, ''
		,td = NR.[NOME]			, ''
		,td = NR.[COORDENACAO]	, ''
		,td = EMAIL.[EMAIL]	    , ''
		,td = NR.[SITUACAO]     , ''
		,td = ISNULL(CONVERT(VARCHAR,NR.[DTCERTIFICACAO],103),'') , ''
		,td = ISNULL(CONVERT(VARCHAR,NR.[DTREVALIDACAO],103),'')  , ''
		,td = ISNULL(CONVERT(VARCHAR,NR.[DTVIGENCIA],103),'')	  , ''
		,td = NR.[VENCIMENTO] , ''
	FROM [vwCOLABORADOR] NR 
		INNER JOIN [TB_EMAILCOORDENACAO] EMAIL ON NR.[COORDENACAO] = EMAIL.[COORDENACAO]  
	WHERE NR.[NORMATIVO] = @NORMATIVO4 
		AND NR.[SITUACAO] IN ('ATIVIDADE NORMAL','FÉRIAS','ESTAGIÁRIO','AFASTADO < = 15 DIAS')
		AND NR.[VENCIMENTO] <= @VENCIMENTO
		--AND  EMAIL.[COORDENACAO] = @COORDENACAO
		AND EMAIL.[EMAIL] = @EMAIL
		ORDER BY NR.[COORDENACAO], NR.[MATRICULA]
				FOR XML PATH('tr'), TYPE) AS NVARCHAR(MAX) ) +
			'</toby></table></body></html>'	

ELSE

 SET @HTML4 = '<hr /><br /><font face="Tahoma" size="2" color ="#004A8F"><strong>NÃO HÁ ' + @NORMATIVO4 + ' PARA VENCER EM ' + CONVERT(NVARCHAR,@VENCIMENTO) + ' DIAS OU MENOS.</strong></font><br /><br />'

/*************************************************************************/
/*                           CONSULTA REG-55                             */
/*************************************************************************/ 
SET @VALIDADOR5 = ( SELECT DISTINCT COUNT(A.[VENCIMENTO]) 
				   FROM [vwCOLABORADOR] A
				   INNER JOIN [TB_EMAILCOORDENACAO] EMAIL ON A.[COORDENACAO] = EMAIL.[COORDENACAO]
				   WHERE A.[NORMATIVO] = @NORMATIVO5 
				   AND A.[VENCIMENTO] <= @VENCIMENTO AND EMAIL.[EMAIL] = @EMAIL
				   AND A.[SITUACAO] IN ('ATIVIDADE NORMAL','FÉRIAS','ESTAGIÁRIO','AFASTADO < = 15 DIAS')
				 )

IF @VALIDADOR5 > 0

SET @HTML5 =
	      N'<html>
			<head>
			<title></title>
				<style type="text/css">
					table { border: 1px; font-family: tahoma; font-size: 12px; }    
					th { background: #e6e6e6; padding-right: 10px; padding-left: 10px; }
					tr { text-align: center; }
					td { padding-right: 10px; padding-left: 10px; }
				</style>
			</head>	
			<body>						
			<hr />					
			<h3>' + @NORMATIVO5 + ' À VENCER</h3>
			<table>
				<thead>
				  <tr style="text-align: center;">
				      <th>MATRÍCULA</th>
			          <th>NOME</th>
			          <th>COORDENAÇÃO</th>
					  <th>E-MAIL</th>
			          <th>SITUAÇÃO</th>
			          <th>DATA DE CERTIFICAÇÃO</th>
			          <th>DATA DE REVALIDAÇÃO</th>
			          <th>VIGÊNCIA</th>
			          <th>VENCIMENTO EM (DIAS)</th>
				  </tr>
				</thead>    
		    <tbody>' +
	CAST( (SELECT
		 td = NR.[MATRICULA]	, ''
		,td = NR.[NOME]			, ''
		,td = NR.[COORDENACAO]	, ''
		,td = EMAIL.[EMAIL]	    , ''
		,td = NR.[SITUACAO]     , ''
		,td = ISNULL(CONVERT(VARCHAR,NR.[DTCERTIFICACAO],103),'') , ''
		,td = ISNULL(CONVERT(VARCHAR,NR.[DTREVALIDACAO],103),'')  , ''
		,td = ISNULL(CONVERT(VARCHAR,NR.[DTVIGENCIA],103),'')	  , ''
		,td = NR.[VENCIMENTO] , ''
	FROM [vwCOLABORADOR] NR 
		INNER JOIN [TB_EMAILCOORDENACAO] EMAIL ON NR.[COORDENACAO] = EMAIL.[COORDENACAO]  
	WHERE NR.[NORMATIVO] = @NORMATIVO5 
		AND NR.[SITUACAO] IN ('ATIVIDADE NORMAL','FÉRIAS','ESTAGIÁRIO','AFASTADO < = 15 DIAS')
		AND NR.[VENCIMENTO] <= @VENCIMENTO
		--AND  EMAIL.[COORDENACAO] = @COORDENACAO
		AND EMAIL.[EMAIL] = @EMAIL
		ORDER BY NR.[COORDENACAO], NR.[MATRICULA]
				FOR XML PATH('tr'), TYPE) AS NVARCHAR(MAX) ) +
			'</toby></table></body></html>'	

ELSE

 SET @HTML5 = '<hr /><br /><font face="Tahoma" size="2" color ="#004A8F"><strong>NÃO HÁ ' + @NORMATIVO5 + ' PARA VENCER EM ' + CONVERT(NVARCHAR,@VENCIMENTO) + ' DIAS OU MENOS.</strong></font><br /><br />'

/*************************************************************************/
/*           PARAMETROS PARA ENVIAR EMAIL POR COORDENAÇÃO                */
/*************************************************************************/
SET @assunto = 'Colaboradores expirados ou a vencer em até ' + CONVERT(NVARCHAR,@VENCIMENTO) + ' dias'

SET @CorpoEmail = @comunicadoHTML + @HTML + @HTML1 + @HTML2 + @HTML3 + @HTML4 + @HTML5

EXEC msdb.dbo.sp_send_dbmail 
@profile_name = 'TREINAMENTO_NORMATIVO',
@recipients = @EMAIL,
--@recipients = 'dbenamor@gmail.com',
@subject = @assunto,	
@body = @CorpoEmail,
@body_format = 'HTML'

/*************************************************************************/
/*                FIM DO CURSOR QUE PEGA EMAIL DA COORDENAÇÃO            */
/*************************************************************************/
    FETCH NEXT FROM CUR_EMAIL INTO @EMAIL ;
    END
    CLOSE CUR_EMAIL;
    DEALLOCATE CUR_EMAIL;
GO
/****** Object:  StoredProcedure [dbo].[sp_VALIDA_EMAIL_COORDENACAO]    Script Date: 15/05/2023 23:48:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_VALIDA_EMAIL_COORDENACAO] AS

DECLARE @ASSUNTO VARCHAR(200)
	   ,@HTML NVARCHAR(MAX)
	   ,@VALIDADOR INTEGER	  	

/*************************************************************************/   
/*        CONDIÇÃO CRIADA PARA VALIDAR O EMAIL DAS COORDENAÇÕES          */
/*************************************************************************/	
SET @validador = (SELECT SUM(CASE WHEN [EMAIL] IS NULL THEN 1 ELSE 0 END) FROM vwEMAILSCOORDENACAO)

/*************************************************************************/ 
/*************************************************************************/	
IF @validador > 0

	SET @HTML =
	      N'<html>
			<head>
			<title></title>
				<style type="text/css">
					table { border: 1px; font-family: tahoma; font-size: 12px; }    
					th { background: #e6e6e6; padding-right: 10px; padding-left: 10px; }
					tr { text-align: center; }
					td { padding-right: 10px; padding-left: 10px; }
				</style>
			</head>	
			<body>								
			<h3>COORDENAÇÃO(ÕES) SEM EMAIL CADASTRADO</h3>
			<hr />
			<table>
				<thead>
				  <tr style="text-align: center;">
				      <th>COORDENAÇÃO</th>
				  </tr>
				</thead>    
		    <tbody>' +
	CAST( (SELECT
				td = [COORDENACAO]	, ''
		   FROM [vwEMAILSCOORDENACAO] 
		   WHERE [EMAIL] IS NULL
		   ORDER BY [COORDENACAO]
		   FOR XML PATH('tr'), TYPE) AS NVARCHAR(MAX) ) +
		   '</toby></table></body></html>'

ELSE

	SET @HTML = '<br><br><br><font face="Tahoma" size="2" color ="#004A8F">NÃO EXISTE(M) COORDENAÇÃO(ÕES) SEM EMAIL CADASTRADO.</font><br><br><br>'

/*************************************************************************/
/*           PARAMETROS PARA ENVIAR EMAIL POR COORDENAÇÃO                */
/*************************************************************************/
SET @assunto = 'Coordenação(ões) sem e-mail cadastrado(s) no sistema'

EXEC msdb.dbo.sp_send_dbmail 
@profile_name = 'TREINAMENTO_NORMATIVO',
@recipients = 'dbenamor@gmail.com',
@subject = @assunto,	
@body = @HTML,
@body_format = 'HTML'
GO
