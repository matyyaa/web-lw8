import {createRouter, createWebHashHistory} from "vue-router";
import SignIn from "../components/SignIn.vue";
import StartPage from "../components/StartPage.vue";
import SignUp from "@/components/SignUp.vue";

const routes = [
    {
        path: "/",
        name: "StartPage",
        component: StartPage,
    },
    {
        path: "/login",
        name: "SignIn",
        component: SignIn,
    },
    {
        path: "/register",
        name: "SignUp",
        component: SignUp
    },
];

export const router = createRouter({
    history: createWebHashHistory(),
    routes, 
})

console.log(router);
