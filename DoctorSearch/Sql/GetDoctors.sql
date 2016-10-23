SELECT [DoctorId]
      ,[Name]
      ,[Birth]
      ,Case When [Sex] = 0 then 'Man' Else 'Lady'End As [Sex]
      ,[TEL]
      ,[AreaCode]
      ,[Score]
  FROM [DoctorDB].[dbo].[Doctor_details]