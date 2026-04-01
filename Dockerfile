# =====================
# Stage 1: Build
# =====================
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# نسخ كل ملفات المشروع
COPY . .

# Build المشروع
RUN dotnet publish Mattger-PL/Mattger-PL.csproj -c Release -o /app

# =====================
# Stage 2: Runtime
# =====================
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app

# نسخ ملفات الـ DLL والـ dependencies من الـ build stage
COPY --from=build /app .

# فتح البورت 80
EXPOSE 80

# Start Command
ENTRYPOINT ["dotnet", "Mattger-PL.dll"]
2️⃣ فين تحطه
