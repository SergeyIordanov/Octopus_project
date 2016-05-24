var VOTE_CLASS_NAME = 'vote';

function sortLikes() {
    var votes = $('.' + VOTE_CLASS_NAME);
    var voteContainer = votes.parent();
    var sotedVotes = votes.sort(compareLikes);

    removeVotesByClass(VOTE_CLASS_NAME);
    voteContainer.append(sotedVotes);
}

// a, b - <p class=job-role>
function compareLikes(a, b) {
    var aLikeCount = parseInt($(a).find('span').text());
    var bLikeCount = parseInt($(b).find('span').text());

    return bLikeCount - aLikeCount;
}

function removeVotesByClass(className) {
    $('.' + className).each(function (el) {
        $(this).remove();
    });
}

// adds votes from array into "to" element 
function addVotes(votes, to) {
    if (to != null) {
        $(to).appendTo(votes);
    }
}