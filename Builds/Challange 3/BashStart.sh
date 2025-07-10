BASE_DIR="$(cd "$(dirname "$0")" && pwd)"

"$BASE_DIR/ServiceQueue/ServiceQueue.exe" &
"$BASE_DIR/ServiceCapture/ServiceCapture.exe" &
"$BASE_DIR/ServiceProcessing/ServiceProcessing.exe" &

wait