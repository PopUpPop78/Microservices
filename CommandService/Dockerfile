FROM mcr.microsoft.com/dotnet/sdk:7.0 as build

WORKDIR /app

COPY ["CommandService/CommandService.csproj", "CommandService/"]
COPY ["Common/Common.csproj", "Common/"]
RUN dotnet restore "CommandService/CommandService.csproj"

COPY . .
WORKDIR "/app/CommandService"
RUN dotnet build "CommandService.csproj" -c Release -o /app/build

FROM build as publish
RUN dotnet publish "CommandService.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0

WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT [ "dotnet", "CommandService.dll" ]

# docker build -t <username/imagename> .
# docker run -p 8080:80 -d <username/imagename>
# docker ps
# docker stop <container id>
# docker start <container id>
# docker push <username/imagename>