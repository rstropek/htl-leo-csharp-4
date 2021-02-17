// Select DOM elements to work with
const welcomeDiv = document.getElementById("WelcomeMessage");
const signInButton = document.getElementById("SignIn");
const profileButton = document.getElementById("seeProfile");
const profileDiv = document.getElementById("profile-div");

function showWelcomeMessage(username) {
    welcomeDiv.innerHTML = `Welcome ${username}`;
    signInButton.setAttribute("onclick", "signOut();");
    signInButton.innerHTML = "Sign Out";
}

function updateProfile(data) {
    profileDiv.innerHTML = ''
    const title = document.createElement('p');
    title.innerHTML = "<strong>Display Name: </strong>" + data.displayName;
    const email = document.createElement('p');
    email.innerHTML = "<strong>Mail: </strong>" + data.mail;
    const phone = document.createElement('p');
    phone.innerHTML = "<strong>Given name: </strong>" + data.givenName;
    const address = document.createElement('p');
    address.innerHTML = "<strong>Surname: </strong>" + data.surname;
    profileDiv.appendChild(title);
    profileDiv.appendChild(email);
    profileDiv.appendChild(phone);
    profileDiv.appendChild(address);
}
