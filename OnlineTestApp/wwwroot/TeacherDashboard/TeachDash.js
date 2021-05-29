const app = Vue.createApp({
    data() {
        return {
            teacherUrl: "https://localhost:44308/api/Teachers/",
            examsUrl: "https://localhost:44308/api/Exams/",
            teacherName: "",
            userId: localStorage.Id,
            userPassword: localStorage.password,
            examsArray: [],
            toggleAddNewTest: false

        };
    
    },
    methods: {



    },
    mounted(){
        fetch(this.teacherUrl + this.userId).then((response) => {
            if (response.ok){
                    return response.json();
                }
            })
            .then((data) =>{
                console.log(data);
                this.teacherName = data.name;
                if (data.password === this.userPassword) {
                    console.log("password match!");
                    //now need to show all his exams: GET all exam by teacher id-
                    fetch(this.examsUrl + this.userId).then((response) => {
                        if (response.ok){
                                return response.json();
                            }
                        })
                        .then((data) =>{
                            
                            
                            var examTemp={
                                id: "",
                                title: "",
                                teacherId: "",
                                dateOfTest: ""
                            }
                            var i = 0;
                            this.examsArray.push.apply(this.examsArray, data);
                            this.examsArray.shift();

                            console.log("new-logs-down");
                            console.log(this.examsArray);
                            console.log(data);

                            


                
                        }) 
                    
                }
    
            }) 


    },
    watch:{}






});
app.component('exam-list-item',{
    props:['exam'],
    template:`<li class="d-flex justify-content-between shadow">
    <div class="d-flex flex-row align-items-center"><i class="fa fa-check-circle checkicon"></i>
        <div class="ml-2">
            <h6 class="mb-0">{{ exam.title }}</h6>
            <div class="d-flex flex-row mt-1 text-black-50 date-time">
                <div><i class="fa fa-calendar-o"></i><span class="ml-2"> {{exam.dateOfTest}}   22 May 2020 11:30 PM</span></div>
                
            </div>
        </div>
    </div>
    <div class="d-flex flex-row align-items-center">
        <div class="d-flex flex-column mr-2">
            <div class="profile-image"> <button type="button" class="btn btn-outline-secondary"><img class="" src="/icons/edit-2.png" width="35"></button> <button type="button" class="btn btn-outline-danger"><img class="" src="/icons/delete-2.png" width="35"></button></div>
        </div> <i class="fa fa-ellipsis-h"></i>
    </div>
</li>`,
    data(){
        return{

        }
    },
    methods:{}

});
app.mount('#TeacherDashboard');

//const Home = app.component('exam-list-item')

//console.log(localStorage);