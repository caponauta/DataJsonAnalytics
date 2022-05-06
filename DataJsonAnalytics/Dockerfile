FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR apijson

EXPOSE 80
EXPOSE 5024

#COPY PROJECT FILES
COPY ./*.csproj ./
RUN	dotnet restore

#COPY EVERYTHING ELSE
COPY . .
RUN dotnet publish -c Release -o out

#Build image
FROM mcr.microsoft.com/dotnet/sdk:5.0
WORKDIR /apijson
COPY --from=build /apijson/out .
ENTRYPOINT ["dotnet","DataJsonAnalytics.dll"]