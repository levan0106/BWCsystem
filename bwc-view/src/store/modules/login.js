import {HTTP} from '@/plugin/http'
import * as types from './type'

const login = {
    namespaced:true,
    state: {
        accessToken: '',
        refreshToken: '',
        tokenExpiresIn:0,
        status: false
    },
    mutations: {
        [types.DO_LOGIN](state, { status, accessToken, refreshToken, tokenExpiresIn }) {
            state.status = status
            state.accessToken = accessToken
            state.refreshToken = refreshToken
            state.tokenExpiresIn = tokenExpiresIn
        },

    },
    actions: {
      doLogin({commit},{user,password}){
        return new Promise((resolve, reject) => {
            
            HTTP.post('authentication', { 
                UserName: user, 
                Password: password 
            }).then(response=>{
                // debugger
                var status = response.status;           
                var accessToken = response.headers["access_token"];
                var refreshToken = response.headers["refresh_token"];
                var tokenExpiresIn = response.headers["expires_in"];
                if (status === 200 && accessToken != '') {
                    commit(types.DO_LOGIN, { 
                        status: true, 
                        accessToken: accessToken, 
                        refreshToken: refreshToken, 
                        tokenExpiresIn: tokenExpiresIn 
                    })
                } else {
                    commit(types.DO_LOGIN, { status: false })
                }
            
                resolve()
            })
            .catch(e=>{
                reject()
            })
        })
      },
     },
    getters: { 
        accessToken: state => state.accessToken,
        refreshToken: state => state.refreshToken,
        tokenExpiresIn: state => state.tokenExpiresIn,
        status: state => state.status,
    }
  }
export default login