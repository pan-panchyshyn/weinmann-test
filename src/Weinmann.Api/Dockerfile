#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Weinmann.Api/Weinmann.Api.csproj", "Weinmann.Api/"]
COPY ["Weinmann.BusinessLogic/Weinmann.BusinessLogic.csproj", "Weinmann.BusinessLogic/"]
COPY ["Weinmann.Core/Weinmann.Core.csproj", "Weinmann.Core/"]
COPY ["Weinmann.Domain/Weinmann.Domain.csproj", "Weinmann.Domain/"]
COPY ["Weinmann.DataAccess/Weinmann.DataAccess.csproj", "Weinmann.DataAccess/"]
RUN dotnet restore "Weinmann.Api/Weinmann.Api.csproj"
COPY . .
WORKDIR "/src/Weinmann.Api"
RUN dotnet build "Weinmann.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Weinmann.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Weinmann.Api.dll"]

# Install Entity Framework CLI
RUN dotnet tool install --global dotnet-ef --version 6.0.0

# Run migrations on startup
CMD /root/.dotnet/tools/dotnet-ef database update && dotnet WebAPI.dll