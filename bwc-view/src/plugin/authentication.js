var ACCESS_TOKEN = "access_token"
var REFRESH_TOKEN = "refresh_token"
var EXPIRES_IN = "expires_in"
var USER_ID = "user_id"
var USER_NAME = "User_Name"

const authentication = {
    system:{
        setAuth (auth) {
            localStorage.setItem(ACCESS_TOKEN, auth.accessToken);
            localStorage.setItem(REFRESH_TOKEN, auth.refreshToken);
            localStorage.setItem(EXPIRES_IN, auth.expiresIn);
            localStorage.setItem(USER_ID, auth.userId);
            localStorage.setItem(USER_NAME, auth.userName);
        },
        isAuthenticated () {
            //alert('unauthorize')
            return localStorage.getItem(ACCESS_TOKEN) != '' && localStorage.getItem(ACCESS_TOKEN) != null;
        },
        currentUser () {
            return localStorage.getItem(USER_ID);
        },
        currentUserProfile () {
            return localStorage.getItem(USER_NAME);
        },
        getAccessToken () {
            return localStorage.getItem(ACCESS_TOKEN);
        },
        getRefreshToken () {
            return localStorage.getItem(REFRESH_TOKEN);
        },
    },
    
}
export default authentication 