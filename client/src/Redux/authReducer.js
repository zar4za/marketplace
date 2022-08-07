let LOG_IN = "LOG-IN";
let LOG_OUT = "LOG-OUT";


let initialState = {
    isAuth: true,
    userId: null,
    profileName: 'BigPatek',
    profilePicture: "https://avatars.akamai.steamstatic.com/272a40a1628e319ffd32946fe213884ae06ca84d_full.jpg"
}

const authReducer = (state = initialState, action) => {
    switch (action.type){
        case LOG_IN:
            return {
                ...state,
                isAuth: true,
                ...action.data
            };
        case LOG_OUT:
            return{
                ...state,
                isAuth: false,
                userId: null,
                profileName: null,
                profilePicture: null
            }
        default:
            return state;
    }
}

export const logInActionCreator = (userId, profileName, profilePicture) => ({type: LOG_IN, data : {userId, profileName, profilePicture}})
export const logOutActionCreator = () => ({type: LOG_OUT});

export default authReducer;