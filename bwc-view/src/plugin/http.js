import axios from 'axios'
import Authentication from '@/plugin/authentication'

export const HTTP = axios.create({
    baseURL: window.bwc.apiUrl,
    headers:{
        'Content-Type': 'application/json; charset=utf-8',        
    },
    //dataType: 'json',
    async:false,
    crossDomain : true,
    transformRequest:[function(data,headers){
        headers['Authorization'] = 'Bearer ' + Authentication.system.getAccessToken()
        return JSON.stringify(data)
    }]
})
HTTP.interceptors.request.use(
    request => { // intercept the global error
        return request
    },
    error =>{
        return Promise.reject(error);
    }
)

HTTP.interceptors.response.use(
    response => { 
        return response
    }, 
    error => {

    let originalRequest = error.config
    //debugger
    originalRequest._retry = false
    if (error.response === undefined && !originalRequest._retry) { 
        originalRequest._retry = true
        window.location.href = window.bwc.rootUrl + '/error'
        return
    }
    if (error.response.status === 401 && !originalRequest._retry) { 
        // if the error is 401 and hasent already been retried
        originalRequest._retry = true

        if (error.response.headers['token_expired'] == '1') {
            
            // Refresh token
            return HTTP.post('authentication/RefreshToken', {
                RefreshToken: Authentication.system.getRefreshToken(),
                AccessToken: Authentication.system.getAccessToken()
            }).then(response => {
                var status = response.status;           
                var accessToken = response.headers["access_token"];
                var refreshToken = response.headers["refresh_token"];
                var tokenExpiresIn = response.headers["expires_in"];
                
                // Update token info
                if (status === 200 && accessToken != '') {
                    Authentication.system.setAuth({
                        accessToken: accessToken,
                        refreshToken: refreshToken,
                        expiresIn: tokenExpiresIn,
                        userId: Authentication.system.currentUser(),
                        userName: Authentication.system.currentUserProfile()
                    })
                }

                if(originalRequest.data != undefined){
                    originalRequest.data = JSON.parse(originalRequest.data);
                }
                return HTTP(originalRequest)

            }).catch(error=>{
                window.location.href = window.bwc.rootUrl + '/login'
                return Promise.reject(error);
            })
        } else {
            window.location.href = window.bwc.rootUrl + '/login'
        }
        return
    }
    if (error.response.status === 404 && !originalRequest._retry) {
      originalRequest._retry = true
      window.location.href = window.bwc.rootUrl + '/404'
      return
    }
    if (error.response.status === 403 && !originalRequest._retry) {
        originalRequest._retry = true
        window.location.href = window.bwc.rootUrl + '/403'
        return
    }
    if (error.response.status === 500 && !originalRequest._retry) {
        originalRequest._retry = true
        window.location.href = window.bwc.rootUrl + '/error/500/'
        return
    }
    // Do something with response error
    return Promise.reject(error)
  })

  export default HTTP