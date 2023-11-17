FROM mcr.microsoft.com/dotnet/sdk:7.0 as build

WORKDIR /app

COPY ["PlatformService/PlatformService.csproj", "PlatformService/"]
COPY ["Common/Common.csproj", "Common/"]
RUN dotnet restore "PlatformService/PlatformService.csproj"

COPY . .
WORKDIR "/app/PlatformService"
RUN dotnet build "PlatformService.csproj" -c Release -o /app/build

FROM build as publish
RUN dotnet publish "PlatformService.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0

# SQL Server trusted connection problem
RUN sed -i 's/CipherString = DEFAULT@SECLEVEL=2/CipherString = DEFAULT@SECLEVEL=1/g' /etc/ssl/openssl.cnf


WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT [ "dotnet", "PlatformService.dll" ]

# docker build -t <username/imagename> .
# docker run -p 8080:80 -d <username/imagename>
# docker ps
# docker stop <container id>
# docker start <container id>
# docker push <username/imagename>