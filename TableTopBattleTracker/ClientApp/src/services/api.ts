
 class Service {

     _apiBase = "/";//"https://www.dnd5eapi.co/api/";

 async getResource(url:string)  {
     const res: Response = await fetch(`${this._apiBase}${url}`);
     console.log("----------------------");
     console.dir(res);

     if(!res.ok) {
         throw new Error ("Could not fetch"+`${res.status}`)
     } 

     let t = await res.json();

 return t; 
}

 async getMonstersList() {
    return await this.getResource(`monsters/`);
} 
/* async getMonstersList() {
    const res= await axios.get(`${this._apiBase}/monsters/`) 
   console.log(res.data.results.results);
    
    return await (res);
} */
getMonster (index:string){
    return this.getResource(`/monsters/${index}`);
}



}
export default (new Service());