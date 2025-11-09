function sendToApp(msg) {
  if (window.chrome && window.chrome.webview) {
    window.chrome.webview.postMessage(msg);
  }
}

document.getElementById("login-btn").addEventListener("click", () => sendToApp("login"));
document.getElementById("launch-btn").addEventListener("click", () => sendToApp("launch_game"));
