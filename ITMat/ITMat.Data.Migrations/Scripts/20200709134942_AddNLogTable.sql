SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[Log] (
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Logged] [datetime] NOT NULL,
	[Level] [nvarchar](50) NOT NULL,
	[User] [nvarchar](50) NULL,
	[Message] [nvarchar](max) NOT NULL,
	[TemplateMessage] [nvarchar](max) NOT NULL,
	[Logger] [nvarchar](250) NULL,
	[Url] [nvarchar](2048) NOT NULL,
	[Action] [nvarchar](100) NOT NULL,
	[QueryString] [nvarchar](1024) NULL,
	[Body] [nvarchar](max) NULL,
	[Exception] [nvarchar](max) NULL,
CONSTRAINT [PK_dbo.Log] PRIMARY KEY CLUSTERED ([Id] ASC)
    WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]