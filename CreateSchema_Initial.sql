--USE [SurveyApp]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 09/12/2012 15:46:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Roles](
 [ID] [int] IDENTITY(1,1) NOT NULL,
 [Name] [varchar](200) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
 [ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, 
    IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Questions]    Script Date: 09/12/2012 15:46:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Questions](
 [ID] [int] IDENTITY(1,1) NOT NULL,
 [Text] [varchar](200) NOT NULL,
 [QuestionType] [varchar](200) NOT NULL,
 [Options] [varchar](2000) NOT NULL,
 CONSTRAINT [PK_Questions] PRIMARY KEY CLUSTERED 
(
 [ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, 
   IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Users]    Script Date: 09/12/2012 15:46:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Users](
 [ID] [int] IDENTITY(1,1) NOT NULL,
 [FirstName] [varchar](200) NOT NULL,
 [LastName] [varchar](200) NULL,
 [UserName] [varchar](200) NOT NULL,
 [Password] [varchar](200) NOT NULL,
 [Role] [int] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
 [ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, 
   IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Surveys]    Script Date: 09/12/2012 15:46:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Surveys](
 [ID] [int] IDENTITY(1,1) NOT NULL,
 [Title] [varchar](200) NULL,
 [Description] [varchar](200) NOT NULL,
 [CreatedOn] [datetime] NOT NULL,
 [ExpiresOn] [datetime] NULL,
 [CreatedBy] [int] NOT NULL,
 [Publish] [bit] NOT NULL,
 CONSTRAINT [PK_Surveys] PRIMARY KEY CLUSTERED 
(
 [ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, 
      IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SurveyResponse]    Script Date: 09/12/2012 15:46:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SurveyResponse](
 [ID] [int] NOT NULL,
 [SurveyID] [int] NOT NULL,
 [QuestionID] [int] NOT NULL,
 [Response] [varchar](200) NOT NULL,
 [FilledBy] [int] NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Survey_Questions]    Script Date: 09/12/2012 15:46:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Survey_Questions](
 [ID] [int] NOT NULL,
 [SurveyID] [int] NOT NULL,
 [QuestionID] [int] NOT NULL,
 [OrderId] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_Survey_Questions_Questions]    Script Date: 09/12/2012 15:46:11 ******/
ALTER TABLE [dbo].[Survey_Questions]  WITH CHECK ADD 
      CONSTRAINT [FK_Survey_Questions_Questions] FOREIGN KEY([QuestionID])
REFERENCES [dbo].[Questions] ([ID])
GO
ALTER TABLE [dbo].[Survey_Questions] CHECK CONSTRAINT [FK_Survey_Questions_Questions]
GO
/****** Object:  ForeignKey [FK_Survey_Questions_Surveys]    Script Date: 09/12/2012 15:46:11 ******/
ALTER TABLE [dbo].[Survey_Questions]  WITH CHECK ADD  
      CONSTRAINT [FK_Survey_Questions_Surveys] FOREIGN KEY([SurveyID])
REFERENCES [dbo].[Surveys] ([ID])
GO
ALTER TABLE [dbo].[Survey_Questions] CHECK CONSTRAINT [FK_Survey_Questions_Surveys]
GO
/****** Object:  ForeignKey [FK_SurveyResponse_Questions]    Script Date: 09/12/2012 15:46:11 ******/
ALTER TABLE [dbo].[SurveyResponse]  WITH CHECK ADD 
      CONSTRAINT [FK_SurveyResponse_Questions] FOREIGN KEY([QuestionID])
REFERENCES [dbo].[Questions] ([ID])
GO
ALTER TABLE [dbo].[SurveyResponse] CHECK CONSTRAINT [FK_SurveyResponse_Questions]
GO
/****** Object:  ForeignKey [FK_SurveyResponse_Surveys]    Script Date: 09/12/2012 15:46:11 ******/
ALTER TABLE [dbo].[SurveyResponse]  WITH CHECK ADD 
      CONSTRAINT [FK_SurveyResponse_Surveys] FOREIGN KEY([SurveyID])
REFERENCES [dbo].[Surveys] ([ID])
GO
ALTER TABLE [dbo].[SurveyResponse] CHECK CONSTRAINT [FK_SurveyResponse_Surveys]
GO
/****** Object:  ForeignKey [FK_SurveyResponse_Users]    Script Date: 09/12/2012 15:46:11 ******/
ALTER TABLE [dbo].[SurveyResponse]  WITH CHECK ADD  
      CONSTRAINT [FK_SurveyResponse_Users] FOREIGN KEY([FilledBy])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[SurveyResponse] CHECK CONSTRAINT [FK_SurveyResponse_Users]
GO
/****** Object:  ForeignKey [FK_Surveys_Users]    Script Date: 09/12/2012 15:46:11 ******/
ALTER TABLE [dbo].[Surveys]  WITH CHECK ADD  CONSTRAINT [FK_Surveys_Users] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Surveys] CHECK CONSTRAINT [FK_Surveys_Users]
GO
/****** Object:  ForeignKey [FK_Users_Roles]    Script Date: 09/12/2012 15:46:11 ******/
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles] FOREIGN KEY([Role])
REFERENCES [dbo].[Roles] ([ID])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Roles]
GO 