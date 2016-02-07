USE [nsappoc2_db]
GO

/****** Object:  View [dbo].[AnswersResult]    Script Date: 5-2-2016 21:10:55 ******/
DROP VIEW [dbo].[AnswersResult]
GO

/****** Object:  View [dbo].[AnswersResult]    Script Date: 5-2-2016 21:10:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[AnswersResult] AS
SELECT 
CategoryId,
PeriodId,
TotalQuestions,
TotalUsers,
TotalQuestions * TotalUsers MaxAnswers,
Performance
FROM (
SELECT q.categoryid CategoryId
, a.periodId PeriodId
, count(DISTINCT q.id) TotalQuestions
, count(DISTINCT a.userid) TotalUsers
, sum(a.value) Performance 
FROM answer a
INNER JOIN question q ON q.id = a.questionId 
GROUP BY a.periodId, q.categoryid
) RESULT
GO


