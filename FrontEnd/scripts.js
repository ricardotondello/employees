const AddRegion = () =>
{
    const response = document.getElementById('region_response');
    const region_id = document.getElementById('region_id').value;
    const region_name = document.getElementById('region_name').value;
    const region_parent_id = document.getElementById('region_parent_id').value;

    const data = {
        id: region_id,
        name: region_name,
        RegionId: region_parent_id ? region_parent_id : null
    }

    console.log('request', JSON.stringify(data))

    fetch('https://localhost:44377/region', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(data),
    })
    .then((response) => response.json())
    .then((data) => {
        console.log('Success:', data);
        if(data.errors){
            response.textContent = JSON.stringify(data);
            return;
        }
        response.textContent = `Region ${data.name} added succefully!`;
    })
    .catch((error) => {
        console.error('Error:', error);
        response.textContent = error;
    });
}


const AddEmployee = () =>
{
    const response = document.getElementById('employee_response');
    const employee_surname = document.getElementById('employee_surname').value;
    const employee_name = document.getElementById('employee_name').value;
    const employee_region_id = document.getElementById('employee_region_id').value;

    const data = {
        name: employee_name,
        surname: employee_surname,
        RegionId: employee_region_id
    }

    console.log('request', JSON.stringify(data))

    fetch('https://localhost:44377/employee', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(data),
    })
    .then((response) => response.json())
    .then((data) => {
        console.log('Success:', data);
        if(data.errors){
            response.textContent = JSON.stringify(data);
            return;
        }
        response.textContent = `Employee (${data.id}) ${data.name} added succefully!`;
    })
    .catch((error) => {
        console.error('Error:', error);
        response.textContent = error;
    });
}

const GetEmployeeUnderRegion = () =>
{
    const response = document.getElementById('get_employee_response');
    const region_id = document.getElementById('get_region_id').value;

    fetch(`https://localhost:44377/region/${region_id}/employees`, {
        method: 'GET',
    })
    .then((response) => response.json())
    .then((data) => {
        console.log('Success:', data);
        if(data.errors){
            response.textContent = JSON.stringify(data);
            return;
        }
        response.textContent = JSON.stringify(data);;
    })
    .catch((error) => {
        console.error('Error:', error);
        response.textContent = error;
    });

}