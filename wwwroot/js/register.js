function clearError(id) {
  const el = document.getElementById(id);
  el.innerText = "";
  el.style.color = "red";
}

async function submitRegister() {
  const fields = [
    "username",
    "password",
    "firstName",
    "lastName",
    "email",
    "phone",
    "birthDate",
    "favoriteCar",
  ];

  let hasError = false;

  // Clear all errors first
  fields.forEach((field) => clearError(field + "Error"));
  clearError("genderError");

  // Validate all inputs
  fields.forEach((field) => {
    const value = document.getElementById(field).value.trim();
    if (!value) {
      const errorEl = document.getElementById(field + "Error");
      errorEl.innerText = "This field is required.";
      errorEl.style.color = "red";
      hasError = true;
    }
  });

  // Validate gender
  const gender = document.querySelector('input[name="gender"]:checked')?.value;
  if (!gender) {
    const genderErr = document.getElementById("genderError");
    genderErr.innerText = "Please select a gender.";
    genderErr.style.color = "red";
    hasError = true;
  }

  if (hasError) return;

  // Create user object
  const user = {
    userName: document.getElementById("username").value.trim(),
    password: document.getElementById("password").value.trim(),
    firstName: document.getElementById("firstName").value.trim(),
    lastName: document.getElementById("lastName").value.trim(),
    email: document.getElementById("email").value.trim(),
    phone: document.getElementById("phone").value.trim(),
    birthDate: document.getElementById("birthDate").value,
    gender,
    favoriteCar: document.getElementById("favoriteCar").value,
  };

  // Send to backend
  const response = await fetch("/api/users/register", {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(user),
  });

  if (response.ok) {
    alert("Registered successfully!");
    window.location.href = "/Users/Login";
  } else {
    alert("Registration failed.");
  }
}
