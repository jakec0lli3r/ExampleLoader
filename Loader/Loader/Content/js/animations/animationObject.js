var animationObject = 
{
    loginShow : function() 
    {
        $("#loader").hide();

        $("#contentArea").animate(
        {
            width: '400px',
            opacity: '1'
        }, 500);	
        $("#contentArea").animate(
        {
            height: '505px'	
        }, 400);
        $("#logo").animate(
        {
            opacity: '1'
        }, 2000);	
    },

    loginHide : function()
    {
        $("#contentArea *").fadeOut();
        $("#contentArea").animate(
        {
            width: '100px',
            opacity: '1'
        }, 500);         	
        $("#contentArea").animate(
        {
            height: '100px'	
        }, 400, function()
        {
            $("#contentArea").fadeOut("fast", function()
            {
                $("#loader").fadeIn(function()
                {
                    setTimeout(function()
                    {
                        $("#loader").fadeOut("fast", function()
                        {
                            $("#contentArea").animate(
                            {
                                width: '400px',
                                opacity: '1'
                            }, 250);	
                            $("#contentArea").animate(
                            {
                                height: '505px'	
                            }, 250);
                            $("#logo").animate(
                            {
                                opacity: '1'
                            }, 1000);	
                            $("#contentArea").fadeIn("fast", function()
                            {
                                animationObject.selectorShow();
                            });
                        });
                    }, 750);
                });
            });
        });
    },

    selectorDebug : function()
    {
        $("#contentArea *").fadeOut();
        $("#contentArea").animate(
        {
            width: '100px',
            opacity: '1'
        }, 0);         	
        $("#contentArea").animate(
        {
            height: '100px'	
        }, 0, function()
        {
            $("#contentArea").fadeOut("fast", function()
            {
                $("#loader").fadeIn(function()
                {
                    setTimeout(function()
                    {
                        $("#loader").fadeOut("fast", function()
                        {
                            $("#contentArea").animate(
                            {
                                width: '400px',
                                opacity: '1'
                            }, 0);	
                            $("#contentArea").animate(
                            {
                                height: '505px'	
                            }, 0);
                            $("#logo").animate(
                            {
                                opacity: '1'
                            }, 0);	
                            $("#contentArea").fadeIn("fast", function()
                            {
                                animationObject.selectorShow();
                            });
                        });
                    }, 0);
                });
            });
        });
    },

    selectorShow : function()
    {
        $("#selectionContainer").show();
        $("#logo, #logoContainer").show();
        $("#selectionContainer *").fadeIn();
    }
}