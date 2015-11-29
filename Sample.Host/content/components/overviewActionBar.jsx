var OverviewActionBar = React.createClass({

  render() {

    return (
      <div className="col-sm-3 well">

        <ul className="nav nav-pills nav-stacked">
          {this.props.children}
        </ul>

      </div>
    );

  }

});
