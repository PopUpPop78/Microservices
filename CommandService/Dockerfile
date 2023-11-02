FROM mcr.microsoft.com/dotnet/sdk:7.0 as build

WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0

WORKDIR /app

COPY --from=build /app/out .

ENTRYPOINT [ "dotnet", "CommandService.dll" ]

# docker build -t <username/imagename> .
# docker run -p 8080:80 -d <username/imagename>
# docker ps
# docker stop <container id>
# docker start <container id>
# docker push <username/imagename>