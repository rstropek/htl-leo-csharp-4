/** 
 * Helper function to call web APIs endpoint
 * using the authorization bearer token scheme
*/
async function getApi(endpoint, token) {
    const headers = new Headers();
    headers.append("Authorization", `Bearer ${token}`);

    const options = {
        method: "GET",
        headers: headers
    };

    const response = await fetch(endpoint, options);
    if (response.status !== 200) throw new Error(`Error while accessing API: ${response.status}`);
    return response.json();
}

async function postApi(endpoint, token, payload) {
    const headers = new Headers();
    headers.append('Authorization', `Bearer ${token}`);
    headers.append('Content-Type', 'application/json');

    const options = {
        method: "POST",
        headers: headers,
        body: JSON.stringify(payload)
    };

    const response = await fetch(endpoint, options);
    if (response.status !== 201) throw new Error(`Error while accessing API: ${response.status}`);
    return response.json();
}
