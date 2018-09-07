-- create database BrighterSqlQueue

CREATE TABLE [dbo].[QueueData](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Topic] [nvarchar](255) NOT NULL,
	[MessageType] [nvarchar](1024) NOT NULL,
	[Payload] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_QueueData] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]