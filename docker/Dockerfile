FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

WORKDIR /App
COPY ./src ./
WORKDIR /App/BookStore.Api

RUN dotnet restore
RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/sdk:6.0

WORKDIR /App/BookStore.Api
COPY --from=build-env /App/BookStore.Api/out .

ENTRYPOINT ["dotnet", "BookStore.Api.dll"]