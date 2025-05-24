function clearError(id) {
  const errorEl = document.getElementById(id);
  errorEl.innerText = "";
  errorEl.style.color = "red";
}

async function loginUser() {
  const username = document.getElementById("username").value.trim();
  const password = document.getElementById("password").value.trim();

  let hasError = false;

  if (!username) {
    const el = document.getElementById("usernameError");
    el.innerText = "Please enter a username.";
    el.style.color = "red";
    hasError = true;
  }

  if (!password) {
    const el = document.getElementById("passwordError");
    el.innerText = "Please enter a password.";
    el.style.color = "red";
    hasError = true;
  }

  if (hasError) return;

  const res = await fetch("/api/users/login", {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ UserName: username, Password: password }),
  });

  if (res.ok) {
    window.location.href = "/Index";
  } else {
    const el = document.getElementById("passwordError");
    el.innerText = "Incorrect username or password.";
    el.style.color = "red";
  }
}
