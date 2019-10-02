#!/usr/bin/env bash
echo "Убиваем предыдущий запуск"
lsof -ti tcp:5000 | xargs kill
lsof -ti tcp:6000 | xargs kill
lsof -ti tcp:7000 | xargs kill
lsof -ti tcp:8000 | xargs kill

echo "Запускаем сервисы"

dotnet run -p First 2>/dev/null &
dotnet run -p Second 2>/dev/null &
dotnet run -p Third 2>/dev/null &
GO111MODULE=on go run ./alien/alien.go
echo "Ожидаем старта"

wait