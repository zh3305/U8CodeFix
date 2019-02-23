select * from ufsystem..ua_account

  select cacc_id,cacc_name from ua_account

select distinct iyear from ua_period where cacc_id='


SELECT [cAcc_Id]
      ,[iBeginYear]
      ,[iEndYear]
      ,[cDatabase]
  FROM [UFSystem].[dbo].[UA_AccountDatabase] where 
'2010' BETWEEN [iBeginYear] AND   (case when [iEndYear] is null  then '9999' else [iEndYear] end)