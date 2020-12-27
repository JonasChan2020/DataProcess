import { getToken, setToken, removeToken } from '@/utils/auth'
import { resetRouter } from '@/router'
import Cookies from 'js-cookie'
import { login, logout, refreshToken, getListMeunFuntionBymeunCode, sysConnect } from '@/api/basebasic'
const getDefaultState = () => {
  return {
    token: getToken(),
    name: localStorage.getItem('username'),
    avatar: localStorage.getItem('useravatar'),
    subSystem: JSON.parse(localStorage.getItem('usersubSystem')),
    activeSystemName: localStorage.getItem('activeSystemName'),
    menus: JSON.parse(localStorage.getItem('nowmenus'))
  }
}

const state = getDefaultState()

const mutations = {
  RESET_STATE: (state) => {
    Object.assign(state, getDefaultState())
  },
  SET_TOKEN: (state, token) => {
    state.token = token
  },
  SET_NAME: (state, name) => {
    state.name = name
    localStorage.setItem('username', name)
  },
  SET_TEMPNAME: (state, name) => {
    localStorage.setItem('usernametemp', name)
  },
  SET_AVATAR: (state, avatar) => {
    state.avatar = avatar
    localStorage.setItem('useravatar', avatar)
  },
  SET_SUBSYSTEM: (state, subSystem) => {
    state.subSystem = subSystem
    localStorage.setItem('usersubSystem', JSON.stringify(subSystem))
  },
  SET_ACTIVESYSTEMNAME: (state, activeSystemName) => {
    state.activeSystemName = activeSystemName
    localStorage.setItem('activeSystemName', activeSystemName)
  },
  SET_MENUS: (state, menus) => {
    state.menus = menus
    localStorage.setItem('nowmenus', JSON.stringify(menus))
  }
}

const actions = {
  userlogin ({ commit }, userInfo) {
    const { username, password, vcode, verifyCodeKey } = userInfo
    return new Promise((resolve, reject) => {
      login({ username: username.trim(), password: password, vcode: vcode, vkey: verifyCodeKey, appId: 'system', systemCode: 'openauth' }).then(response => {
        const data = response.ResData
        commit('SET_TOKEN', data.AccessToken)
        setToken(data.AccessToken)
        commit('SET_TEMPNAME', data.Account)
        commit('SET_AVATAR', data.HeadIcon)
        commit('SET_SUBSYSTEM', data.SubSystemList)
        commit('SET_ACTIVESYSTEMNAME', data.ActiveSystem)
        commit('SET_MENUS', data.MenusList)
        commit('SET_NAME', data.Account)
        Cookies.set('yuebon_loginuser', data.UserId)
        resolve(response)
      }).catch(error => {
        console.log('000:' + JSON.stringify(error))
        reject(error)
      })
    })
  },
  // 系统切换
  sysConnetLogin ({ commit }, userInfo) {
    return new Promise((resolve, reject) => {
      sysConnect(userInfo).then(response => {
        const data = response.ResData
        commit('SET_TOKEN', data.AccessToken)
        setToken(data.AccessToken)
        commit('SET_TEMPNAME', data.Account)
        commit('SET_AVATAR', data.HeadIcon)
        commit('SET_SUBSYSTEM', data.SubSystemList)
        commit('SET_ACTIVESYSTEMNAME', data.ActiveSystem)
        commit('SET_MENUS', data.MenusList)
        commit('SET_NAME', data.Account)
        Cookies.set('yuebon_loginuser', data.UserId)
        resolve(response)
      }).catch(error => {
        reject(error)
      })
    })
  },
  // user logout
  logout ({ commit, state }) {
    return new Promise((resolve, reject) => {
      logout(state.token).then(() => {
        removeToken() // must remove  token  first
        localStorage.removeItem('username')
        localStorage.removeItem('useravatar')
        localStorage.removeItem('usersubSystem')
        localStorage.removeItem('activeSystemName')
        localStorage.removeItem('nowmenus')
        Cookies.remove('yuebon_loginuser')
        resetRouter()
        commit('RESET_STATE')
        resolve()
      }).catch(error => {
        removeToken() // must remove  token  first
        localStorage.removeItem('username')
        localStorage.removeItem('useravatar')
        localStorage.removeItem('usersubSystem')
        localStorage.removeItem('activeSystemName')
        localStorage.removeItem('nowmenus')
        Cookies.remove('yuebon_loginuser')
        resetRouter()
        commit('RESET_STATE')
        reject(error)
      })
    })
  },
  // remove token
  resetToken ({ commit }) {
    const data = {
      'token': getToken()
    }
    removeToken() // must remove  token  first
    return new Promise(resolve => {
      refreshToken(data).then(res => {
        const data = res.ResData
        commit('SET_TOKEN', data.AccessToken)
      })
      commit('RESET_STATE')
      resolve()
    })
  },
  userNowMeunsFun (encode) {
    const code = encode
    return new Promise((resolve, reject) => {
      getListMeunFuntionBymeunCode({ enCode: code }).then(response => {
        const data = response.ResData
        localStorage.setItem('yuebon_currentmenus', JSON.stringify(data))
        resolve(response)
      }).catch(error => {
        reject(error)
      })
    })
  }
}

export default {
  namespaced: true,
  state,
  mutations,
  actions
}

