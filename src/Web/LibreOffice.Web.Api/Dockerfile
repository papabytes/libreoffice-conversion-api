FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY . .
RUN dotnet restore

WORKDIR /app/src/Web/LibreOffice.Web.Api
RUN dotnet publish -c Release --runtime linux-x64  --self-contained true  -o out

FROM papabytes/ubuntu-libreoffice:18.04
WORKDIR /app
COPY --from=build /app/src/Web/LibreOffice.Web.Api/out .

EXPOSE 80
ENV SOFFICE_BIN_PATH="/usr/bin/soffice"

CMD ["/app/LibreOffice.Web.Api", "--urls", "http://0.0.0.0:80"]