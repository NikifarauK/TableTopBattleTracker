import React, { useEffect, useState } from "react";
import { parseJsonText } from "typescript";
import Service from "../services/api";
interface Moster {
    id: string;
    name: string;
    url: string;
}

const SearchBar: React.FC = () => {
    const [list, setList] = useState<string>();
    const [lists, setLists] = useState();
    let monsters = null;
    useEffect(() => {
        const monstersList = async () => {
            const Arr = await Service.getMonstersList();
            setList(JSON.stringify(Arr))
            monsters = Arr.map((monster: Moster) => {
                return (<p key={monster.id}> <a href={'https://dnd.su/' + monster.url} target="_blank">{monster.name}</a></p>)
            })
            setLists(monsters)
        }
        monstersList()



    }, [])

    return (<div>{lists}</div>)
}


export default SearchBar;