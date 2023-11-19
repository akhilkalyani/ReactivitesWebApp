import { Button, Card,Image } from "semantic-ui-react";
import { Activity } from "../../../app/models/activitity";

interface Prop{
    activity:Activity;
    cancelSelectActivity:()=>void;
    openForm:(id:string)=>void;
}
export default function ActivityDetails({activity,cancelSelectActivity,openForm}:Prop){
    return(
        <Card fluid>
            <Image src={`/assets/categoryImages/${activity.category}.jpg`} wrapped ui={false} />
            <Card.Content>
                <Card.Header>{activity.title}</Card.Header>
                <Card.Meta>
                    <span>{activity.date}</span>
                </Card.Meta>
                <Card.Description>
                    {activity.description}
                </Card.Description>
            </Card.Content>
            <Card.Content extra>
                <Button.Group widths={2}>
                    <Button onClick={()=>openForm(activity.id)} basic color='blue' content='Edit'/>        
                    <Button onClick={cancelSelectActivity} basic color='blue' content='Cancel'/>        
                </Button.Group>
            </Card.Content>
        </Card>
    )
}