FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

EXPOSE 80
EXPOSE 443

#COPY PROJECT FILES
COPY ./*.csproj ./
RUN dotnet restore 

#COPY EVERYTHING ELSE

COPY . .
RUN dotnet publish -c Release -o out
RUN dotnet dev-certs https --trust
#BUILD IMAGE
FROM mcr.microsoft.com/dotnet/sdk:6.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet","Convertidor.dll"]
