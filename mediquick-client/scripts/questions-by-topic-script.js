const questionsCollection = document.getElementsByClassName('question');
const questionsArray = Array.from(questionsCollection);
questionsArray.forEach(question => {
    question.addEventListener('click', () => {
        // Close all other divs
        questionsArray.forEach(d => {
            if (d !== question) {
                d.classList.remove('open');
                d.classList.add('closed');
            }
        });

        // Toggle the clicked div
        if (question.classList.contains('open')) {
            question.classList.remove('open');
            question.classList.add('closed');
        } else {
            question.classList.remove('closed');
            question.classList.add('open');
        }
    });
});

const heartIconCollection = document.getElementsByClassName('icon');
const heartIconArray = Array.from(heartIconCollection);
heartIconArray.forEach(icon => {
    icon.addEventListener('click', (event) => {
        event.stopPropagation();
        if(icon.classList.contains('fav')) {
            icon.src = './../images/icons/empty-heart.svg';
            icon.classList.remove('fav');
            //----adding to favourites code will be here----
        } else {
            icon.src = './../images/icons/full-heart.svg';
            icon.classList.add('fav');
            //----removing from favourites code will be here----
        }
        
    })
})