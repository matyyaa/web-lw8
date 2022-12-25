import Vuex from 'vuex'

const store = new Vuex.Store({
    state: {
        user: null,
        token: null,
    },
    mutations: {
        setUser(state, user) {
            state.user = user
        }
        ,setToken(state, token) {
            state.token = token
        }
    },
    actions: {},
    getters: {
        isLoggedIn(state) {
            return !!state.token;
        },
        getToken(state) {
            return state.token;
        }
    }
})

export default store;