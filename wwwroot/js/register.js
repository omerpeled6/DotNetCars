async function submitRegister() {
  const user = {
    userName: document.getElementById("username").value.trim(),
    password: document.getElementById("password").value.trim(),
    firstName: document.getElementById("firstName").value.trim(),
    lastName: document.getElementById("lastName").value.trim(),
    email: document.getElementById("email").value.trim(),
    phone: document.getElementById("phone").value.trim(),
    birthDate: document.getElementById("birthDate").value,
    gender: document.querySelector('input[name="gender"]:checked')?.value,
    favoriteCar: document.getElementById("favoriteCar").value,
  };

  // Check required
  for (let key in user) {
    if (!user[key]) {
      alert("Please fill in all fields.");
      return;
    }
  }

  // Send to server
  const response = await fetch("/api/users/register", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(user),
  });

  if (response.ok) {
    alert("Registered successfully!");
    window.location.href = "/Users/Login";
  } else {
    alert("Registration failed.");
  }
}
