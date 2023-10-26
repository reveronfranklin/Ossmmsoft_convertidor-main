FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR convertidor

EXPOSE 80
EXPOSE 5000

#COPY PROJECT FILES
COPY ./*.csproj ./
#RUN dotnet restore 

#COPY EVERYTHING ELSE

COPY . .
RUN dotnet publish -c Release -o out

#BUILD IMAGE
FROM mcr.microsoft.com/dotnet/sdk:6.0
WORKDIR /convertidor
COPY --from=build /convertidor/out .
ENTRYPOINT ["dotnet","Convertidor.dll"]
