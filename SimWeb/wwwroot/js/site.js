function nextTurn() {
    fetch('/?handler=NextTurn', {
        method: 'POST',
    })
        .then(response => response.json())
        .then(data => {
            location.reload();
        });
}
