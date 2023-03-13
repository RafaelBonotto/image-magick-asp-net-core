#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ImageMagickExample.Api/ImageMagickExample.Api.csproj", "ImageMagickExample.Api/"]
RUN dotnet restore "ImageMagickExample.Api/ImageMagickExample.Api.csproj"
COPY . .
WORKDIR "/src/ImageMagickExample.Api"
RUN dotnet build "ImageMagickExample.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ImageMagickExample.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ImageMagickExample.Api.dll"]