// Instantiate object used to perform auth for SPAs.
// For more details see documentation at
// https://azuread.github.io/microsoft-authentication-library-for-js/ref/classes/_azure_msal_browser.publicclientapplication.html
//
const myMSALObj = new msal.PublicClientApplication(msalConfig);

// Here we store our username
let username = '';

function selectAccount() {
    // Get all currently signed-in accounts.
    // For more details see documentation at
    // https://azuread.github.io/microsoft-authentication-library-for-js/ref/classes/_azure_msal_browser.publicclientapplication.html#getallaccounts
    const currentAccounts = myMSALObj.getAllAccounts();
    if (currentAccounts.length === 0) return;
    else if (currentAccounts.length === 1) {
        // To keep things simple, we only support a single account here.
        username = currentAccounts[0].username;
        showWelcomeMessage(username);
        return;
    }

    console.error('Multiple accounts are not supported here');
}

async function signIn() {
    try {
        // Login using a popup window. For more details see
        // https://azuread.github.io/microsoft-authentication-library-for-js/ref/classes/_azure_msal_browser.publicclientapplication.html#loginredirect
        // Note that redirecting to the login page would also be possible (with loginRedirect()).
        // This would help if popups are not wanted. However, not covered in this simple example.
        const response = await myMSALObj.loginPopup(loginRequest);
        if (response !== null) {
            username = response.account.username;
            showWelcomeMessage(username);
            return;
        }

        console.error('Could not log in. Note that multiple accounts are not supported here.');
    } catch (error) {
        console.error(error);
    }
}

async function signOut() {
    // Logout. For more details see documentation at
    // https://azuread.github.io/microsoft-authentication-library-for-js/ref/classes/_azure_msal_browser.publicclientapplication.html#logout
    const logoutRequest = { account: myMSALObj.getAccountByUsername(username) };
    await myMSALObj.logout(logoutRequest);
}

async function getTokenPopup(request) {
    try {
        // Try to get an access token silently. This will used a cached token if available.
        // Otherwise it will try to get a new one. If a new token cannot be acquired, the method
        // will fail. For more details see documentation at
        // https://azuread.github.io/microsoft-authentication-library-for-js/ref/classes/_azure_msal_browser.publicclientapplication.html#acquiretokensilent
        request.account = myMSALObj.getAccountByUsername(username);
        return await myMSALObj.acquireTokenSilent(request);
    } catch (error) {
        console.warn('Silent token acquisition fails. acquiring token using popup');
        if (error instanceof msal.InteractionRequiredAuthError) {
            // fallback to interaction when silent call fails
            try {
                // Try to get a token using a popup window. For more details see
                // https://azuread.github.io/microsoft-authentication-library-for-js/ref/classes/_azure_msal_browser.publicclientapplication.html#acquiretokenpopup
                const tokenResponse = await myMSALObj.acquireTokenPopup(request);
                return tokenResponse;
            } catch (error) {
                console.error(error);
            }
        } else {
            console.warn(error);
        }
    }
}

async function seeProfile() {
    try {
        // Get token for graph API and call graph API for user profile. For more details see
        // https://docs.microsoft.com/en-us/graph/api/user-get
        const response = await getTokenPopup(graphRequest);
        const graphResponse = await getApi('https://graph.microsoft.com/v1.0/me', response.accessToken);
        updateProfile(graphResponse);
    } catch (error) {
        console.error(error);
    }
}

async function getCustomApi() {
    try {
        // Get token for custom API and call it.
        const response = await getTokenPopup(customApiRequest);
        const apiResponse = await getApi('http://localhost:5000/api/orders', response.accessToken);
        console.dir(apiResponse);
    } catch (error) {
        console.error(error);
    }
}

selectAccount();
