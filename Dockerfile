# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# انسخ كل الملفات
COPY . .

# ثبت الـ MySQL EF Core package فقط داخل الـ Docker build
RUN dotnet add src/Mattger-PL/Mattger-PL.csproj package Pomelo.EntityFrameworkCore.MySql --version 8.0.9

# بعد كده اعمل restore و publish
RUN dotnet restore src/Mattger-PL/Mattger-PL.csproj
RUN dotnet publish src/Mattger-PL/Mattger-PL.csproj -c Release -o /app

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "Mattger-PL.dll"]
