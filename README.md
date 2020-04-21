# LibreOffice Api

Tired of looking for ways to Convert Office documents to PDF, or Image formats?

Well despair no more. This repository contains a somewhat "solution" to this problem. Using as base [LibreOffice](https://www.libreoffice.org/) "soffice" command cli wrapped around a simple .NetCore Api.


## Setup Development environment
In order to run and work on this project you will need a proper IDE of your choice (i.e. [VSCode](https://code.visualstudio.com/) or [Visual Studio Community](https://visualstudio.microsoft.com/vs/community/)) and the following:

*  [NetCore 3.1 SDK](https://dotnet.microsoft.com/download)
*  [LibreOffice](https://www.libreoffice.org/download/download/)


Set the environment variable SOFFICE_BIN_PATH to the location of your soffice binary. If the path contains spaces, take those into consideration i.e: in Windows the path should be wrapped in " i.e: "C:\Program Files\LibreOffice\program\soffice".

## I don't want to develop anything I just want to use it.

This project is available as a docker image [here](https://hub.docker.com/repository/docker/papabytes/libreoffice-api).

The image exposes the port 80/tcp as default and you can easily have it running by typing in your console

 `docker run -itd --name libreoffice-api -p 8080:80 papabytes/libreoffice-api:latest`

## Reminders

Please take into consideration that whole magic is not done by this project but by the amazing work of the **LibreOffice** workforce so, if you can please show them some love [here](https://www.libreoffice.org/donate/).