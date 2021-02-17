// Initialize MSAL.js 2.x app
// For more details see https://docs.microsoft.com/en-us/azure/active-directory/develop/msal-js-initializing-client-applications#initialize-msaljs-2x-apps
//
const msalConfig = {
    auth: {
        // Client ID of our web app in AAD. I have registered this web app in our school's
        // AAD for you. We will take a look at it during our lesson.
        clientId: "f1c162e7-75cc-45af-95bb-dc0419a285a3",

        // Login URL of our school's AAD tenant. During our lesson, we will find out
        // our schools AAD tenant ID.
        authority: "https://login.microsoftonline.com/91fc072c-edef-4f97-bdc5-cfb67718ae3a/",

        // URL where AAD schould deliver tokens to. Not really relevant in this simple example.
        redirectUri: "http://localhost:3000",
    },
    cache: {
        cacheLocation: "sessionStorage", // This configures where your cache will be stored
        storeAuthStateInCookie: false,   // Set this to "true" if you are having issues on IE11 or Edge
    },
    system: {	
        loggerOptions: {	
            loggerCallback: (level, message, containsPii) => {	
                if (containsPii) {		
                    return;		
                }		
                switch (level) {		
                    case msal.LogLevel.Error:		
                        console.error(message);		
                        return;		
                    case msal.LogLevel.Info:		
                        console.info(message);		
                        return;		
                    case msal.LogLevel.Verbose:		
                        console.debug(message);		
                        return;		
                    case msal.LogLevel.Warning:		
                        console.warn(message);		
                        return;		
                }	
            }	
        }	
    }
};

/**
 * Scopes you add here will be prompted for user consent during sign-in.
 * By default, MSAL.js will add OIDC scopes (openid, profile, email) to any login request.
 * For more information about OIDC scopes, visit: 
 * https://docs.microsoft.com/en-us/azure/active-directory/develop/v2-permissions-and-consent#openid-connect-scopes
 */
const loginRequest = {
    scopes: ["User.Read", "api://14f9a758-cdba-47ba-8178-c0d54de0ab88/read", "api://14f9a758-cdba-47ba-8178-c0d54de0ab88/write"]
};

const graphRequest = {
    scopes: ["User.Read"]
};

const customApiRequest = {
    scopes: ["api://14f9a758-cdba-47ba-8178-c0d54de0ab88/read", "api://14f9a758-cdba-47ba-8178-c0d54de0ab88/write"]
};
